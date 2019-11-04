
namespace Threenine.Print.Simple
{
    
    /// <summary>
    /// Simple Implementation of the Singleton Pattern.
    /// Not Thread safe and not good practice
    /// DO NOT USE
    /// </summary>
   public sealed class Spooler : Spool
   {
       private static Spooler instance;
   
     
       public static Spooler Instance => instance ??= new Spooler();
       
   }
}