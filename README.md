🚀 PackIt

PackIt es una aplicación de escritorio pensada para comprimir archivos y carpetas de manera rápida y sencilla. Genera archivos ZIP (y otros formatos con librerías externas) directamente desde la interfaz WPF, usando .NET 8 y C#.

✨ Funcionalidades

📂 Comprimir archivos individuales

📁 Comprimir carpetas completas

💾 Guardar archivos comprimidos en la ubicación que el usuario elija

⚡ Interfaz moderna y fácil de usar con WPF

🧩 Posibilidad de agregar más formatos de compresión mediante librerías externas

🛠 Tecnologías utilizadas

Lenguaje: C#

Framework: .NET 8

Plataforma: WPF (Windows Presentation Foundation)

Librerías clave:

System.IO.Compression → compresión ZIP nativa

Microsoft.Win32.SaveFileDialog → selección de ubicación para guardar archivos

📂 Estructura del proyecto

ViewModels: lógica de compresión y binding con la Vista

Views (XAML): interfaz de usuario

Models: representan archivos y carpetas, encapsulando datos

Helpers/Servicios: clases para manejar Streams y generar ZIPs
