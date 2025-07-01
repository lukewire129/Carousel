# CarouselView for WPF 🎠

**WPF 환경에서 자연스러운 슬라이딩 전환과 인디케이터 UI를 지원하는 경량 Carousel 컨트롤입니다.**  
불필요한 외부 종속성 없이 MVVM 구조에 최적화되어 있으며, 커스터마이징 가능한 템플릿 구조를 통해 다양한 UX에 유연하게 대응할 수 있도록 설계되었습니다.

---

## ✨ 주요 기능

- 부드러운 좌우 슬라이딩 애니메이션
- `ItemTemplate`을 통한 유연한 콘텐츠 구성
- 선택 상태를 시각적으로 표시하는 인디케이터(점)
- `ItemsSource`, `SelectedItem` 바인딩 지원 (MVVM 친화적)
- 완전한 XAML 스타일링 및 템플릿 오버라이드 지원
- 종속성 없음 (Pure WPF)

---

## 🖼️ 사용 예시

```xml
<carousel:CarouselView ItemsSource="{Binding MyItems}"
                       SelectedItem="{Binding CurrentItem, Mode=TwoWay}">
    <carousel:CarouselView.ItemTemplate>
        <DataTemplate>
            <Grid Width="200" Height="120">
                <!-- 사용자 정의 콘텐츠 -->
                <TextBlock Text="{Binding Title}" FontSize="24" />
            </Grid>
        </DataTemplate>
    </carousel:CarouselView.ItemTemplate>
</carousel:CarouselView>
```
## 🔧 작동 원리
- ContentPresenter 2개를 활용하여 애니메이션 전환 시 기존 콘텐츠와 신규 콘텐츠를 병렬로 구성하고,
- TranslateTransform을 통해 슬라이딩 애니메이션을 구현합니다.
- 인디케이터는 ItemsSource의 항목 수에 맞춰 자동 생성되며, 현재 선택된 항목의 위치에 따라 점 색상이 실시간으로 변경됩니다.

## 📁 주요 구성 요소
| 파일명                   | 설명                                                         |
|--------------------------|--------------------------------------------------------------|
| `CarouselView.cs`        | 캐러셀 컨트롤의 핵심 로직을 담고 있는 사용자 지정 컨트롤 클래스 |
| `CarouselView.xaml`      | `ControlTemplate`, 버튼, 인디케이터 스타일 등 UI 정의        |
| `IndicatorItem.cs`       | 인디케이터의 선택 상태를 표현하는 내부 ViewModel            |
| `BoolToBrushConverter.cs`| 인디케이터의 선택 여부에 따른 색상 전환용 컨버터            |

## 🎯 개발 배경
WPF는 모바일 UI에서 자주 사용되는 CarouselView와 같은 컨트롤을 기본 제공하지 않습니다.
기존 서드파티 라이브러리는 지나치게 무겁거나 유연성이 떨어지는 경우가 많아, 직접 MVVM 친화적이고 가벼운 컨트롤을 제작하게 되었습니다.

이 컨트롤은 다음을 목표로 설계되었습니다:

- 애니메이션 흐름이 자연스러울 것
- MVVM 아키텍처에 완전히 통합 가능할 것
- XAML 기반 사용자 정의가 쉬울 것
- 외부 종속성이 없을 것

## ⚙️ 요구 사항
- .NET 6 이상

- WPF 애플리케이션 환경

## 📄 라이선스
이 프로젝트는 MIT License로 배포되며,
개인 및 상업적 프로젝트에서 자유롭게 사용 및 수정이 가능합니다.

## 🙋 문의 및 기여
피드백, 버그 제보, 개선 아이디어는 언제든지 GitHub Issue나 PR로 환영합니다.
함께 더 나은 CarouselView를 만들어보아요!
