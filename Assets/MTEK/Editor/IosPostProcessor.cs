using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;
#if UNITY_IPHONE
using UnityEditor.iOS.Xcode;
#endif


namespace MTEK.Editor
{
    public class IosPostProcessor
    {
#if UNITY_IPHONE
            static PBXProject proj;
            static string projPath;
            static string targetGuid;
            static string mainGuid;
        [PostProcessBuild]
        public static void OnPostProcessBuild(BuildTarget target, string path)
        {

        if(target != BuildTarget.iOS) { return; }

        projPath = Path.Combine(path, "Unity-iPhone.xcodeproj/project.pbxproj");

        // automatically @include frameworks and disable bitcode
        proj = new PBXProject();
        string file = File.ReadAllText(projPath);
        proj.ReadFromString(file);

#if UNITY_2019_3_OR_NEWER
        targetGuid = proj.GetUnityFrameworkTargetGuid();
        mainGuid = proj.GetUnityMainTargetGuid();
#else
        targetGuid = proj.TargetGuidByName(proj.GetUnityTargetName());
        mainGuid = proj.TargetGuidByName(proj.GetUnityMainTargetGuid());
#endif

        proj.SetBuildProperty(targetGuid, "CLANG_ENABLE_MODULES", "YES");
        proj.SetBuildProperty(mainGuid, "CLANG_ENABLE_MODULES", "YES");
        proj.SetBuildProperty(targetGuid, "ENABLE_BITCODE", "NO");
        BlendProjects();
        File.WriteAllText(projPath, proj.WriteToString());

        // add necessary permissions to Plist
        string plistPath = Path.Combine(path, "Info.plist");
        PlistDocument plist = new PlistDocument();
        plist.ReadFromString(File.ReadAllText(plistPath));

        PlistElementDict rootDict = plist.root;
        rootDict.SetString("NSPhotoLibraryUsageDescription", "Requires access to the Photo Library");
        rootDict.SetString("NSPhotoLibraryAddUsageDescription", "Requires access to the Photo Library");
        rootDict.SetString("NSCameraUsageDescription", "Requires access to the Camera");
        rootDict.SetString("NSContactsUsageDescription", "Requires access to Contacts");
        rootDict.SetString("NSLocationAlwaysUsageDescription", "Requires access to Location");
        rootDict.SetString("NSLocationWhenInUseUsageDescription", "Requires access to Location");
        rootDict.SetString("NSLocationAlwaysAndWhenInUseUsageDescription", "Requires access to Location");

        

        File.WriteAllText(plistPath, plist.WriteToString());

        }
        public static void BlendProjects()
        {
// Set data folder target membership to UnityFramework
            string dataGuid = proj.FindFileGuidByProjectPath("Data");
            proj.AddFileToBuild(targetGuid, dataGuid);

// Set NativeCallProxy target membership to UnityFramework
            string nativeCallGuid = proj.FindFileGuidByProjectPath("Libraries/Plugins/iOS/NativeCallProxy.h");
            proj.AddPublicHeaderToBuild(targetGuid, nativeCallGuid);
            
            //proj.SetBuildProperty(targetGuid,"ATTRIBUTES","Public");
        }
#endif
    }
}