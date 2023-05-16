
using Password_Tester.Test_Helpers;

var internalSec = new InteralSecurity();
//var testPassword = "Hello, World!";
var testPassword = "test";
var encrypted = internalSec.PasswordEncryption.EncryptPassword(testPassword);
Console.WriteLine($"Encrypted = {encrypted}");
Console.ReadKey();