namespace Consystent
{
  namespace Sounds 
  {
    public interface ISoundTeller
    {
      protected void PlaySound (string soundName);
      
      protected void PlaySound (Sound newSound);
    }
  }
}
