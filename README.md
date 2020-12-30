# GalanthasEncryptedNotes
project I made for myself as an encrypted note / password manager.  No warranty!

Best way to install:
[https://galantha.net/appPublishGalanthasEncryptedNotes/publish.htm](https://galantha.net/appPublishGalanthasEncryptedNotes/publish.htm)
If you wish to compile and build yourself, it was built using Visual Studio 2019 C# Winforms.

I made this project as secure as I was able to do.


It works by taking the password you enter when opening or making a new file, and through the use of an algromithm does a one way converstion to a secret key.  That secret key is then used to generate differant secret keys for the encrypted note items by combining it with what is refered to as as salt.  Each of these salted keys then in turn are used to create a 256 bit encyrption and decryption keys for each of the note items that are stored.


The whole password database is then saved to the disk using a differant secret key generated in a similar manner.


You can see how this is done for the note items in the frmMain.cs function NoteKey_DtKeysForNote_rowChanged.
For the Note Data items frmMain.cs function NoteData_SaveNoteData.
For the entire file, the GalLib.cs function EncryptDataSetToFile, and EncryptDataSetToFileAsyncHelper.


The function used to merge the hashes is microsofts managed sha512.  The encryption decryption is microsofts managed Rijndael 256.  I went with Rijndael over AES for the 256 bit block sizes.

The rest of the program uses autosizing winforms that should scale nicely to any screen size.  Parts of the UI are multithreaded because the key generation/decryption is to performance expensive to be done in the primary thread.  

Clipboard cut / paste is supported.
