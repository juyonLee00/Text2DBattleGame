# Text2DBattleGame
콘솔로 구현한 배틀 게임입니다.
* 팀명 : Life IS Egg
* 팀장 : 이선재
* 팀원 : 박민혁, 유채연, 이주연

<br/>
<br/>

## 데모 영상
유튜브에서 시연영상을 확인하실 수 있습니다 : [시연영상](https://youtu.be/lVmllD-2MiQ) 

<br/>
<br/>

## 규칙
### 1) git, branch 규칙
- PR올릴 때 기능에 대한 코멘트 작성하기
- branch 명명: feature/유저명/기능명


<br/>

### 2) Commit Convention
- [type] body
- type
    - feat : 새로운 파일 생성, 새로운 기능 추가
    - fix : 버그 수정
    - refactor : 코드 리팩토링
    - chore : 동작에 영향을 주지 않는 사항들 (주석, 정렬 등등)
- body : 구현한 기능 설명

<br/>
<br/>

## 역할
### 이선재
- 캐릭터 스킬 시스템 구현
    - 각 직업별 스킬 및 기능 추가
    - 전투 시스템에 적용

- 배틀 중 치명타, 회피 추가
    - 스킬은 회피불가
    - 캐릭터 치명타확률, 치명타 데미지 추가
    - 각자가 가진 회피율로 적 또는 플레이어가 회피

- 상점 시스템 구현
    - 아이템 가격 별 구입, 판매 가능
    - 구입 중복 가능


<br/>

### 박민혁
- 던전(레벨업)
- 던전(보상 추가)
- 회복 아이템 

<br/>

### 유채연
- 전투 시작
- 스테이지 추가
<details>
<summary>몬스터 생성방식</summary>
 몬스터 종류는 미니언 공허충 대포미니언 타락거미 그림자 타락거미여왕 수중뱀 수중서펀트 드래곤 총 9종류 입니다.

던전레벨
1레벨 : 미니언, 공허충, 대포미니언 3종류에서 1에서 4마리 등장<br/>
2레벨 : 미니언, 공허충, 대포미니언 3종류에서 1에서 5마리 등장<br/>
3레벨 : 공허충, 대포미니언 2종류에서 1에서 4마리 등장<br/>
4레벨 : 공허충, 대포미니언 2종류에서 1에서 5마리 등장<br/>
5레벨 : 대포미니언 4마리 등장<br/>
6레벨 : 타락거미, 그림자, 타락거미여왕 3종류에서 1에서 4마리 등장<br/>
7레벨 : 타락거미, 그림자, 타락거미여왕 3종류에서 1에서 5마리 등장<br/>
8레벨 : 그림자, 타락거미여왕 2종류에서 1에서 4마리 등장<br/>
9레벨 : 그림자, 타락거미여왕 2종류에서 1에서 5마리 등장<br/>
10레벨 : 타락거미여왕 4마리 등장<br/>
11레벨 : 수중뱀, 수중서펀트, 드래곤 3종류에서 1에서 4마리 등장<br/>
12레벨 : 수중뱀, 수중서펀트, 드래곤 3종류에서 1에서 5마리 등장<br/>
13레벨 : 수중서펀트, 드래곤 2종류에서 1에서 4마리 등장<br/>
14레벨 : 수중서펀트, 드래곤 2종류에서 1에서 5마리 등장<br/>
15레벨 : 드래곤 4마리 등장<br/>
16레벨 : 모든 몬스터 종류에서 1에서 4마리 등장<br/>
17레벨 : 모든 몬스터 종류에서 2에서 5마리 등장<br/>
18레벨 : 모든 몬스터 종류에서 3에서 6마리 등장<br/>
16레벨 이상부터는 최소, 최대 마리수가 20,23까지 1마리씩 증가
</details>

<br/>

### 이주연 
- 프로젝트 초기 설정
- 아이템 데이터 생성 및 로드
    - JSON 형식으로 아이템 데이터를 로드.
- 장비 아이템 장착, 장착 해제 
    - 같은 속성의 아이템의 경우 중복 장착 불가능
- 인벤토리 구현
    - 아이템 고유의 속성 확인 가능.
    - 장착한 아이템의 경우 장착하지 않은 아이템 개수 확인 가능
- 게임시작 시 플레이어 데이터 세팅
    - 플레이어 이름 공백처리 및 오버플로우 처리

<br/>
<br/>

## 사용 기술
- C# 8
- .NET 4.8.1

<br/>
<br/>

## Contributors
<div>
<a href="https://github.com/plumas90">
  <img src="https://github.com/plumas90.png" width="50" height="50" >
</a>
    <a href="https://github.com/Lyrwhitt">
  <img src="https://github.com/Lyrwhitt.png" width="50" height="50" >
</a>
    <a href="https://github.com/juyonLee00">
  <img src="https://github.com/juyonLee00.png" width="50" height="50" >
</a>
 <a href="https://github.com/ychy0006">
  <img src="https://github.com/ychy0006.png" width="50" height="50" >
</a>


