# server-stressbot-example
서버 부하테스트를 위한 스트레스봇 예제

UI 제작을 위해 사용한 System.Windows.Forms가 참조를 통해 사용할 수 없어 프로젝트 파일 편집을 통해 사용가능하도록 하였음

프로젝트 파일(.csproj)에
  <<ItemGroup>
    <FrameworkReference Include="Microsoft.WindowsDesktop.App"/>
  </ItemGroup>>
를 추가해서 오류를 없애고 모호한 함수에 대해서는 명확하게 정의함
