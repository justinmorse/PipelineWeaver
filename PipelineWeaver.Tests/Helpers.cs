using System;

namespace PipelineWeaver.Tests.Ado;

public static class Helpers
{

    public static string PATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Desktop", "testPipeline.yaml");


    public class AdoTestObject
    {
        public string Field1 { get; set; } = "Value1";
        public string Field2 { get; set; } = "Value2";
        public bool Field3 { get; set; } = false;
    }
}
