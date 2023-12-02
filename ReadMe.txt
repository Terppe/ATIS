*Recommended Markdown Viewer: [Markdown Editor](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.MarkdownEditor2)*

## Getting Started

Browse and address `TODO:` comments in `View -> Task List` to learn the codebase and understand next steps for turning the generated code into production code.

Explore the [WinUI Gallery](https://www.microsoft.com/store/productId/9P3JFPWWDZRC) to learn about available controls and design patterns.

Relaunch Template Studio to modify the project by right-clicking on the project in `View -> Solution Explorer` then selecting `Add -> New Item (Template Studio)`.

## Publishing

For projects with MSIX packaging, right-click on the application project and select `Package and Publish -> Create App Packages...` to create an MSIX package.

For projects without MSIX packaging, follow the [deployment guide](https://docs.microsoft.com/windows/apps/windows-app-sdk/deploy-unpackaged-apps) or add the `Self-Contained` Feature to enable xcopy deployment.

## CI Pipelines

See [README.md](https://github.com/microsoft/TemplateStudio/blob/main/docs/WinUI/pipelines/README.md) for guidance on building and testing projects in CI pipelines.

## Changelog

See [releases](https://github.com/microsoft/TemplateStudio/releases) and [milestones](https://github.com/microsoft/TemplateStudio/milestones).

## Feedback
Beispiel
IronPDF v2023.3 Release Notes:
•	Adds: IronPdfEngine Docker Support! 
•	Adds: PDF incremental saving
•	Adds: Resize API for scaling page dimensions while keeping aspect ratios
•	Adds: DrawDividerLineColor in Text Header/Footer
•	Updates: Completely reworked PDF signing and signatures (see https://ironpdf.com/how-to/signing/)
•	Updates: IronSoftware.System.Drawing to 2023.2.12
•	Updates CEF 110.0.31 with many security and performance improvements
•	Fixes: Memory leak when using ReplaceTextOnPage
•	Fixes: Stamper and watermark positioning on PDFs with different orientation
•	Fixes: DrawDividerLine bugs in Header/Footer
•	Fixes: Issue with text-wrap in HTML header and footer text not breaking a word correctly when rendering a PDF from HTML. Previously, some words would be broken in unexpected places, resulting in text that was difficult to read or understand
•	Fixes: Bug in the ImageStamper function, which was not reading in relative paths correctly
•	Fixes: Issue where converting an image to PDF would sometimes result in the image not fitting properly within the PDF document.
•	Fixes: Exception when retrieving form fields from a PDF which contains a hyperlink
•	Changes: Minimum .NET Framework version from 4.0 to 4.6.2


Bugs and feature requests should be filed at https://aka.ms/templatestudio.

Version ohne ContentDialog zum ändern, neu, copieren, delete
Klassisch wie V7.6 bzw 7.7

 Anzeige von Listview und Scrollviewer nur wenn datensatz gefunden wird
 Visibility="{x:Bind ViewModel.IsLoading, Mode=OneWay}"> geht nur mit codebehind empty Textbox
         ReferenceExpertCombo.Text = string.Empty; combobox disablen


Diese Version läuft mit BitMiracle.Docotic
LicenseManager.AddLicenseData("6TKB9-WAOW7-PIDGW-YXIRC-OIW9R"); leider nur 31 Tage oder Lizenz kaufen

Dafür QuestPDF ok

SearchQuick ok mit QuestPDF   
Images bearbeitung ok
MapControl fehlt
Fehler hinter TextBlock_DirectChildren
Selected Item ist erster datensatz bei Doppeltapped

}

