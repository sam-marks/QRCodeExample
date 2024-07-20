Create a New Solution and Project:

```
dotnet new sln -n QRCodePOC
dotnet new console -n QRCodeExample
dotnet sln QRCodePOC.sln add QRCodeExample/QRCodeExample.csproj
```

Add Required Packages:
(These may need to be different depending on your platform given the platform specific implementation of Drawing libraries)
```
cd QRCodeExample
cd QRCodeExample

dotnet add package ZXing.Net
dotnet add package SkiaSharp
dotnet add package ZXing.SkiaSharp 
```

