# 🏢 Commercial Automation System (Ticari Otomasyon)

A comprehensive enterprise-grade Windows Forms desktop application developed in C# utilizing **DevExpress UI components** and **SQL Server** for end-to-end commercial operations and business management.

## 📌 About the Project

Commercial Automation is a robust desktop system designed to manage daily business processes including product inventory, customer accounts (Cari), employee tracking, invoicing, and financial cash flow. 

The application features a modern, dashboard-driven multi-form architecture powered by **DevExpress** UI controls to deliver a professional user experience, coupled with relational database operations via **ADO.NET** and SQL Server.

## ✨ Features

- **Product & Category Management:** Add, update, delete, and list inventory items with dynamic pricing and stock controls.
- **Customer (Cari) Accounts:** Manage customer records, city/region statistics, and financial tracking.
- **Employee & Department Management:** Keep track of personnel details, assignments, and roles.
- **Invoicing & Sales Operations:** Process commercial invoices, track transactions, and manage cash flow.
- **Advanced UI with DevExpress:** Clean and responsive data presentation using DevExpress `GridControl`, custom layouts, and modern navigation hubs.
- **Relational Database Integration:** Seamless connectivity with SQL Server utilizing parameterized T-SQL queries and stored procedures.

## 🛠️ Technologies Used

- **Language:** C# (.NET Framework)
- **Framework:** Windows Forms (WinForms)
- **UI Component Library:** DevExpress WinForms Suite
- **Database:** Microsoft SQL Server / Azure SQL Database
- **Data Access:** ADO.NET (`SqlCommand`, `SqlDataAdapter`, `DataTable`)

## 🗄️ Database Schema Overview

The application connects to a SQL Server database containing core relational tables:
- `Tbl_Urunler` (Product inventory, buying/selling prices, stocks)
- `Tbl_Kategoriler` (Product category classifications)
- `Tbl_Musteri` (Customer details and geographical locations)
- `Tbl_Personel` (Employee information and departments)
- `Tbl_FaturaBilgi` & `Tbl_FaturaDetay` (Invoice headers and line items)

**Note:** Ensure your local SQL Server instance is running and update your connection string inside the application configuration before launching.

## 🚀 Getting Started

1. Clone or download this repository.
2. Open the solution file (`.sln`) in **Visual Studio**.
3. Ensure **DevExpress components** are installed and referenced properly in your environment.
4. Update the SQL Server connection string to match your local or cloud database instance.
5. Build and run the application (`F5`).

## 🎯 Learning Objectives

This project helped practice:

- Developing large-scale commercial desktop applications using C# Windows Forms.
- Integrating professional UI component libraries like **DevExpress**.
- Managing complex relational database schemas and multi-table joins.
- Implementing secure data access layers with ADO.NET and parameterized queries.
- Structuring multi-form enterprise architectures cleanly.

## 🔮 Future Improvements

- Migrate local SQL Server backend to cloud services (Azure SQL Database).
- Implement role-based authentication and authorization (Admin vs. Staff).
- Add comprehensive reporting modules (PDF/Excel exports for invoices and sales).
- Move hardcoded connection strings securely to `App.config`.

---

**Developer:** [ugursema](https://github.com/ugursema)
