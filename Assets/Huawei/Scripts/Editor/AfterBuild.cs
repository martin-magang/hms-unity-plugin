using System.IO;
using UnityEditor.Android;
using UnityEngine;

namespace HuaweiService.Editor
{
    public class AfterBuild : IPostGenerateGradleAndroidProject
    {
        public int callbackOrder => 0;
        public void OnPostGenerateGradleAndroidProject(string path)
        {
            Debug.Log($"[OnPostGenerateGradleAndroidProject] Path:{path}");
            var launcherPath = GETOutputPath(path);
            Debug.Log(launcherPath);

            Debug.Log($"Application.dataPath:{Application.dataPath}");
            //读取源文件路径
            var sourceParh = Application.dataPath + "/Plugins/Android/agconnect-services.json";
            //拷贝文件(源路径及文件名, 拷贝路径及文件名, 若该文件名已存在,是否替换)
            File.Copy(sourceParh, launcherPath + "/agconnect-services.json", true);
        }

        private static string GETOutputPath(string path){
            Debug.Log($"UnityVersion: {Application.unityVersion}");
            if (Application.unityVersion.StartsWith("2018")) {
                return path;
            }
            var s = path.Split('/');
            s[s.Length - 1] = "launcher";
            return string.Join("/", s);
        }
    }
}
