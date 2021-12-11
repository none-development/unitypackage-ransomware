# .unitypackage Ransomware


<p align="center">
  <img src="https://cdn.discordapp.com/attachments/820443161167003678/820930234365771786/Unbenannt-2.png">
</p>

# Project Infos

<p align="center">
  <img alt="GitHub code size in bytes" src="https://img.shields.io/github/languages/code-size/Neko-Oneechan/unitypackage-Ransomware?style=for-the-badge">
  <img alt="GitHub release (latest by date including pre-releases)" src="https://img.shields.io/github/v/release/Neko-Oneechan/unitypackage-Ransomware?include_prereleases&style=for-the-badge">
  <img alt="GitHub all releases" src="https://img.shields.io/github/downloads/Neko-Oneechan/unitypackage-Ransomware/total?color=%230099cc&style=for-the-badge">
</p>

# About

This is a proof of concept to encrypt files using a unitypackage.

**This script should not be used to encrypt other systems but should be used for study purposes.**

!!I assume no liability if you encrypt your files during testing!!

!!!I assume no liability for property damage and personal injury thereby!!!

# Support Me

This Concept is made by Mei-chan, to support my Work:

<p align="center">
 <a href="https://www.paypal.me/meichan968" target="_blank"><img alt="Paypal" src="https://img.shields.io/badge/Donate-Paypal-blue?style=for-the-badge"></a>
  <img alt="BTC" src="https://img.shields.io/badge/BTC-1APsMH6sARzX8h3GPW4d4NLnf8JUkP7SzQ-blue?style=for-the-badge">
</p>

or

Follow me on [Github](https://github.com/Neko-Oneechan)

For any support send me a e-mail: `admin@astronyia.xyx`

# General information

### Rules about this project
* Dont use that to create Maleware
* Dont Abuse it
* Do not use it to harm others. This is only meant to test it on your virtual machine.
* I forbid any use of this code to use it on anyone other than yourself

By Download this project you accept that!

# Guide and How to use this to test it

### Step-by-Step Guide to use the Script

* Download this Script
* Add the script to your future .unitypackage folder
* Search a thing that is used in the script like OnGUI or similar 
* Add a new thread there
* The thread is supposed to trigger the methode `Startencrypt` with the bool `true`
* Then start the thread in the function
* Pack the Unitypackage.


**This script uses a password for each file, so it is impossible to decrypt them**

How it look after Run the Ransomware

<p align="center">
  <img src="https://cdn.discordapp.com/attachments/804802833786208278/821362481980899338/unknown.png">
</p>

# Future Updates

### Todo/next updates
- [x] Simple Ransomware
- [x] Support Random String as Password 
- [x] Random Salt Generator
- [x] Random File Extensions
- [x] File Extensions Chinese Strings
- [x] Download other Ransomware to Encrypt after Unity Close
- [x] Run Downloaded Ransomeware after Download
- [x] Test package with decrypter
- [x] Webhook Support to store the Key to decrypt your VM
- [ ] Create Local Password-Salt Note Version
- [ ] Store Filepassword to a webapplication in local network
