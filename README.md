# Warehouse Management System

A console-based inventory management application written in C#.

## Overview

This project allows users to manage products in a warehouse or store inventory.

The system supports adding, editing, searching, deleting and displaying products while storing data in text files.

The goal of the project is to demonstrate object-oriented programming, file handling and data management using C#.

---

## Features

### Product Management

- Add new products
- Edit existing products
- Delete products
- Display all products
- Search products

### Data Storage

- Save products to file
- Load products from file
- Persistent storage between program executions

### Product Information

Each product contains:

- Product Code
- Product Name
- Quantity
- Shelf Life
- Product Group
- Date
- Storage Position

---

## Technologies Used

- C#
- .NET
- Object-Oriented Programming (OOP)
- Lists
- Classes and Objects
- Constructors
- File Handling
- StreamReader
- StreamWriter

---

## Project Structure

### Product Class

Represents a product stored in the inventory.

Properties:

```csharp
Code
Name
Quantity
ShelfLife
Group
Date
Position
```

### Program Class

Contains:

- Menu system
- User interaction
- Product management logic
- File operations

---

## Main Functionalities

### Add Product

Allows the user to enter information for a new product and add it to the inventory.

### Edit Product

Allows modification of existing product information.

### Delete Product

Removes a product from the inventory.

### Search Product

Searches for products by selected criteria.

### Show Products

Displays all stored products.

### Save Products

Stores inventory information into a text file.

### Load Products

Loads inventory information from a text file when the application starts.

---

## Example Menu

```text
1. Add Product
2. Show Products
3. Search Product
4. Edit Product
5. Delete Product
6. Save Products
7. Exit
```

---

## Example Product

```text
Code: 1001
Name: Milk
Quantity: 50
Shelf Life: 7
Group: G
Date: 15.05.2026
Position: A1
```

---

## Learning Objectives

This project was created to practice:

- Object-Oriented Programming
- Classes and Objects
- Constructors
- Properties
- Lists
- Loops
- Conditional Statements
- Methods
- File Handling
- Data Persistence
- Git
- GitHub

---

## Future Improvements

Possible future improvements:

- Password protected access
- Database integration
- JSON file storage
- Product categories
- Expiration date alerts
- Sorting and filtering
- GUI version using WinForms or WPF

---

## Author

Geno Dimitrov

GitHub:
https://github.com/genodimitrov-dev