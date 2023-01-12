using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
   public void Go()
   {
      Time.timeScale = Time.timeScale==0f ? 1f: 0f;
   }
   public void Restart1()
   {
      SceneManager.LoadScene("1 lab");
   }
   public void Restart3()
   {
      SceneManager.LoadScene("3");
   }
   public void Restart4()
   {
      SceneManager.LoadScene("4");
   }
   public void Restart5()
   {
      SceneManager.LoadScene("5");
   }
   public void Restart62()
   {
      SceneManager.LoadScene("6 2");
   }public void Restart61()
   {
      SceneManager.LoadScene("6 1");
   }
   public void Restart7()
   {
      SceneManager.LoadScene("7");
   }
   public void Restart71()
   {
      SceneManager.LoadScene("7 1");
   }
   public void Restart82()
   {
      SceneManager.LoadScene("8 2");
   }
   public void Restart822()
   {
      SceneManager.LoadScene("8 2 lenses");
   }
   public void setPos1()
   {
      GameObject obj = GameObject.Find("Main Capsule");
      obj.transform.position = new Vector3(2f, 0.6f, 0f);
      obj.transform.rotation = Quaternion.Euler(0f,0f,90f);
   }
   public void setPos2()
   {
      GameObject obj = GameObject.Find("Main Capsule");
      obj.transform.position = new Vector3(-3.7f, 0.2f, 0f);
      obj.transform.rotation = Quaternion.Euler(0f,0f,105f);
   }
}
