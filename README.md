# Card to Card Transaction System

This project is a simplified **Card-to-Card Transaction System** for a bank, designed and implemented as part of a programming assignment.  
The system manages bank cards, transactions, user authentication, and supports secure money transfers between cards.

---

## 📌 Features

### Models & Database
- **Card**
  - `CardNumber` (string, 16 digits)
  - `HolderName` (string)
  - `Balance` (float)
  - `IsActive` (bool)
  - `Password` (string)
- **Transaction**
  - `TransactionId` (int)
  - `SourceCardNumber` (string)
  - `DestinationCardNumber` (string)
  - `Amount` (float)
  - `TransactionDate` (date)
  - `IsSuccessful` (bool)

---

### 🔄 Card-to-Card Operations
- **Transfer Function**
  - Input: Source card number, destination card number, transfer amount.
  - Validates:
    - Card numbers are valid.
    - Sufficient balance in source card.
  - Updates balances accordingly.
  - Stores transaction in the `Transaction` table.

---

### ✅ Input Validation
- Card number must be **16 digits**.
- Transfer amount must be **greater than zero**.

---

### 📊 Reporting
- **GetTransactions Function**
  - Input: Card number.
  - Returns all transactions (both source and destination).

---

### 🔐 User Authentication
- Users must log in with **CardNumber + Password** before making transfers.
- Incorrect login prevents transfer.
- If a wrong password is entered **3 times in a row**, the card will be **blocked**.

---
