# 🚀 PackIt

![.NET](https://img.shields.io/badge/.NET-8-blue) ![C#](https://img.shields.io/badge/C%23-9C27B0?logo=csharp&logoColor=white) ![WPF](https://img.shields.io/badge/WPF-Visual_Studio-lightgrey)  

**PackIt** es una aplicación de escritorio diseñada para **comprimir archivos y carpetas** de manera rápida y sencilla. Permite generar archivos ZIP (y otros formatos mediante librerías externas) desde una interfaz moderna en **WPF**, usando **.NET 8 y C#**.

---

## ✨ Funcionalidades
- 📂 Comprimir **archivos individuales**  
- 📁 Comprimir **carpetas completas**  
- 💾 Guardar archivos comprimidos en la ubicación que el usuario elija  
- ⚡ Interfaz moderna y amigable con **WPF**  
- 🧩 Posibilidad de agregar más formatos de compresión con librerías externas  

---

## 🛠 Tecnologías utilizadas
- **Lenguaje:** C#  
- **Framework:** .NET 8  
- **Plataforma:** WPF (Windows Presentation Foundation)  
- **Librerías clave:**  
  - `System.IO.Compression` → compresión ZIP nativa  
  - `Microsoft.Win32.SaveFileDialog` → seleccionar ubicación de guardado  

---

## 📂 Estructura del proyecto
- **ViewModels:** Lógica de compresión y binding con la vista  
- **Views (XAML):** Interfaz de usuario  
- **Models:** Representan archivos y carpetas, encapsulando datos  
- **Helpers/Servicios:** Manejo de Streams y generación de archivos ZIP  

---
