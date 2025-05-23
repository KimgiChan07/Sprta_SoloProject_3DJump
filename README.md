이 프로젝트는 Unity 기반의 3D 플랫폼 게임에서 자주 사용되는 플레이어 상호작용 시스템을 구현합니다.
점프 충전, 낙하 대미지, 점프 패드, 움직이는 플랫폼, 체력/스태미나 관리 등 다양한 물리 기능이 포함되어 있습니다.


# 빌드파일
<details>
<summary>링크</summary>

https://drive.google.com/file/d/1U1oYDnDB1678hsj4sJ_ssl_oXuQrRVcj/view?usp=sharing
  
</details>

##
## 📂 프로젝트 구성
- Assets/
- Scripts/

-  ├── PlayerCondition.cs         # 체력, 스태미나, 점프 게이지

-   ├── PlayerFallDamage.cs       # 낙하 대미지 감지

-   ├── JumpPad.cs                # 점프대

-   ├── MovingPad.cs              # 자동 이동 플랫폼

-   ├── IDamageIbe.cs             # 데미지 인터페이스

-   ├── Condition.cs              # 수치/바 UI 관리

-   └── PlayerJumpUiCondition.cs  # 점프 게이지 UI 바 연동

## 낙하 대미지 (PlayerFallDamage.cs)
Rigidbody의 Y 속도가 기준 이상일 때 착지 순간 피해 적용

IDamageIbe 인터페이스를 통해 상태 시스템과 느슨하게 연결됨

<details>
<summary>코드</summary>

```cs
private void Update()
{
    bool isGrounded = IsGrounded();

    if (isGrounded && !wasGroundedLastFrame)
    {
        float fallSpeed = -rb.velocity.y;

        if (fallSpeed > minFallSpeed)
        {
            float damage = (fallSpeed - minFallSpeed) * damageMultiplier;
            damageIbe?.TakePhysicalDamage(damage);
        }
    }

    wasGroundedLastFrame = isGrounded;
}
```
</details>

## 점프 충전 시스템 (PlayerCondition.cs + PlayerJumpUiCondition.cs)
점프 키 누르고 있으면 게이지 충전

일정 시간 뒤 더 강한 점프

UI 게이지도 실시간 연동됨

<details>
<summary>코드</summary>
  
```cs
public bool IsJumpCharge()
{
    isCharging = true;
    jumpCharge = 0f;
    return true;
}

public float EndJumpCharge()
{
    isCharging = false;
    float ratio = Mathf.Clamp01(jumpCharge / maxChargeTime);
    float force = Mathf.Lerp(minJumpForce, maxJumpForce, ratio);
    stamina.curValue = 0f;
    return force;
}

private void Update()
{
    if (isCharging)
    {
        jumpCharge += Time.deltaTime;
        PlayerJumpUiCondition ui = FindAnyObjectByType<PlayerJumpUiCondition>();
        if (ui != null)
            ui.SetRatio(jumpCharge / maxChargeTime);
    }
}

```

</details>

## 점프 패드 (JumpPad.cs)
플레이어가 Trigger 안에 들어오면 위로 발사

<details>
<summary>코드</summary>

```cs
private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }
}
```
</details>

## 움직이는 플랫폼 (MovingPad.cs)
Inspector에서 Horizontal / Vertical 방향 설정

Mathf.PingPong을 사용해 왕복 이동

Ray로 발 밑을 감지한 경우에만 SetParent() 처리 → 옆면 접촉 무시

<details>
<summary>코드</summary>

```cs
private void Update()
{
    float offset = Mathf.PingPong(Time.time * speed, moveDistance);
    Vector3 newPos = startPos;

    if (moveDirection == MoveDirection.Vertical)
        newPos.y += offset;
    else
        newPos.x += offset;

    transform.position = newPos;
}

private void OnCollisionStay(Collision other)
{
    if (!other.gameObject.CompareTag("Player")) return;

    Vector3 origin = other.transform.position + Vector3.up * 0.1f;
    if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, 0.2f) &&
        hit.collider.gameObject == this.gameObject)
    {
        other.transform.SetParent(transform);
    }
}
```
</details>

## 체력 / 스태미나 시스템 (Condition.cs + UiCondition.cs)
수치 기반으로 curValue, maxValue 관리

Image.fillAmount로 UI에 자동 반영
  

<details>
<summary>코드</summary>

```cs
public void Set(float _value)
{
    curValue = Mathf.Clamp(_value, 0f, maxValue);
    UpdateUI();
}

void UpdateUI()
{
    if (uiBar != null)
        uiBar.fillAmount = curValue / maxValue;
}

```
</details>

## 인터페이스 기반 데미지 처리 (IDamageIbe.cs)
피해를 받는 모든 오브젝트는 IDamageIbe만 구현하면 동작

PlayerCondition이 이 인터페이스를 구현

<details>
<summary>코드</summary>

```cs
public interface IDamageIbe
{
    void TakePhysicalDamage(float damage);
}

public class PlayerCondition : MonoBehaviour, IDamageIbe
{
    public void TakePhysicalDamage(float damage)
    {
        health.Subtract(damage);
        onTakeDamage?.Invoke();
    }
}
```
</details>
