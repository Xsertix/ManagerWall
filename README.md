# ManagerWall

**ManagerWall** is a local password manager written in **C# / .NET**.

The project started as an educational experiment focused on encryption and file handling, but gradually evolved into a functional console-based password manager with secure local storage, account management, and a styled user interface.

## Features

- 🔐 Master password for vault access
- 🔑 AES-256 encryption
- 🧂 Unique salt for each vault
- 🛡️ PBKDF2 for deriving a cryptographic key from the master password
- ⚡ 600,000 PBKDF2 iterations
- 💾 Encrypted local storage
- 📄 JSON serialization of accounts
- ➕ Add accounts
- 👀 View saved accounts
- 🗑️ Delete vault
- 🎨 Styled console interface
- 🐛 Bug fixes and input/error handling

## Security

ManagerWall uses multiple layers of protection:

1. The user sets a master password.
2. A random salt is generated for each vault.
3. A cryptographic key is derived from the master password and salt using PBKDF2.
4. The data is encrypted using AES-256.
5. The encrypted data is stored locally.

Using PBKDF2 with a high number of iterations makes brute-force attacks against the master password significantly more expensive.

## Technologies

- C#
- .NET
- AES-256
- PBKDF2 / HMAC-SHA256
- JSON
- System.Security.Cryptography

## Current Status

🟢 **Core functionality is implemented.**

ManagerWall is now a functional educational password manager featuring encryption, password-based key derivation, encrypted local storage, account management, and a styled console interface.

The project is still under active development. Future updates will focus on improving the architecture, security, testing, and user experience.

## Roadmap

- 🔒 AES-GCM for additional data integrity protection
- ⚠️ Improved error handling
- 🧪 Unit tests
- 🖥️ WPF interface
- 🏗️ Architecture improvements
- 📦 Additional account management features
- 🎣 Website address storage and phishing protection

## Important

ManagerWall was created primarily as an **educational project for learning C#, .NET, cryptography, file handling, and application architecture**.

The current version has several security measures in place, but it has not undergone an independent security audit. For highly sensitive or critical passwords, using a well-established and audited password manager is recommended.

## Author

The project is being developed gradually — from a simple console-based password manager into a more complete application focused on security, reliability, and code quality.
