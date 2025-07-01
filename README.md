# CarouselView for WPF 🎠

**A lightweight Carousel control for WPF with smooth slide transitions and indicator UI.**  
Optimized for MVVM architecture with no external dependencies, and fully customizable via XAML templates to adapt to various UX designs.

---

## ✨ Features

- Smooth left/right sliding animations
- Flexible content layout with `ItemTemplate`
- Visual indicator dots to represent selection state
- MVVM-friendly: supports `ItemsSource` and `SelectedItem` bindings
- Fully customizable through XAML styling and template overrides
- No external dependencies (Pure WPF)

---

## 🖼️ Example Usage

```xml
<carousel:CarouselView ItemsSource="{Binding MyItems}"
                       SelectedItem="{Binding CurrentItem, Mode=TwoWay}">
    <carousel:CarouselView.ItemTemplate>
        <DataTemplate>
            <Grid Width="200" Height="120">
                <!-- Custom content -->
                <TextBlock Text="{Binding Title}" FontSize="24" />
            </Grid>
        </DataTemplate>
    </carousel:CarouselView.ItemTemplate>
</carousel:CarouselView>
```
## 🔧 How It Works
- Uses two ContentPresenters to manage the current and next content side-by-side during animation
- Uses TranslateTransform to animate the sliding transition
- Indicator dots are generated based on the number of items in ItemsSource, and highlight the selected index dynamically

## 📁 Project Structure
| File	                    | Description                                                           |
|---------------------------|-----------------------------------------------------------------------|
| `CarouselView.cs`	        | Main logic for the custom Carousel control                            |
| `CarouselView.xaml`	    | UI definition including ControlTemplate, navigation buttons, and dots |
| `IndicatorItem.cs`	    | ViewModel for representing the selection state of indicator dots      |
| `BoolToBrushConverter.cs`	| Converter for changing indicator dot color based on selection state   |

## 🎯 Motivation
WPF does not provide a built-in CarouselView control like those found in mobile UI frameworks.
Most third-party libraries are either too heavy or not flexible enough, so this project was created with the following goals:

- Smooth and natural animation flow
- Seamless integration with MVVM architecture
- Easy customization via XAML
- No external dependencies

## ⚙️ Requirements
- .NET 6 or later
- WPF application environment

## 📄 License
This project is distributed under the MIT License.
Free to use, modify, and redistribute for both personal and commercial projects.

## 🙋 Feedback & Contributions
Feel free to open issues or submit pull requests for suggestions, bug reports, or improvements.
Let’s make CarouselView better together!

📘 This README is also available in [Korean](./README.ko.md)