이 프로젝트는 Unity 기반의 3D 플랫폼 게임에서 자주 사용되는 플레이어 상호작용 시스템을 구현합니다.
점프 충전, 낙하 대미지, 점프 패드, 움직이는 플랫폼, 체력/스태미나 관리 등 다양한 물리 기능이 포함되어 있습니다.


📂 프로젝트 구성
Assets/
├── Scripts/
│   ├── PlayerCondition.cs         # 체력, 스태미나, 점프 게이지
│   ├── PlayerFallDamage.cs       # 낙하 대미지 감지
│   ├── JumpPad.cs                # 점프대
│   ├── MovingPad.cs              # 자동 이동 플랫폼
│   ├── IDamageIbe.cs             # 데미지 인터페이스
│   ├── Condition.cs              # 수치/바 UI 관리
│   └── PlayerJumpUiCondition.cs  # 점프 게이지 UI 바 연동
