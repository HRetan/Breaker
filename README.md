# Breaker

2018.12.26 note
Title -> Stage Select Scene change Button 기능 추가
씬이동 기능 추가(Title->Stage->InGame, Title->MapTool)
StageManager, GameManager, UIController, SaveNLoad(함수추가) 생성

Stage Button 선택시 해당 Map Load 하도록 만드는 중(Build 후 Load가 안됨)

수정 해야 할 것
Build 후 Load 처리

2019.01.03 note
android 시범 Test
MapTool Save/ Load 기능 추가
MapTool UI 기능 추가

수정 해야 할 것
Save / Load 경로 변경 해야 할듯 (StreamingAssets(Android에서 Save가 안됨) -> persistentDataPath 로)


2019.01.04 note
1. Stage, InGame Save / Load 수정 완료
- 기본적으로 제공하는 Date 같은 경우에는 StreamingAssets를 통해 사용
- 게임 플레이 도중 Save / Load 경우에는 persistentDataPath를 사용
- StageCheck 같은 경우 초기화 후 Load를 통한 값변경
- 클리어시 자동 Save를 통해 클리어 여부 저장

2. StageSelect 씬 UI 기능 추가
- Scroll을 통한 Stage설정

3. InGame 터치를 통한 Player 이동 추가

2019.01.07 note
1. InGame Clear, Failed시 UI 출력 및 씬 전환
- UI 출력시 모든 Update를 정지 시켜줌
2. Block Break Effect 
