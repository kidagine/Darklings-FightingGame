using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEditor.TestTools.TestRunner.Api;

public class ResultCollector : ICallbacks
{
    public ITestResultAdaptor Result { get; private set; }

    public void RunFinished(ITestResultAdaptor result)
    {
        Result = result;
    }

    public void RunStarted(ITestAdaptor testsToRun) { }
    public void TestFinished(ITestResultAdaptor result) { }
    public void TestStarted(ITestAdaptor test) { }
}

public class RunValidationTestsBeforeBuild : IPreprocessBuildWithReport
{

    public int callbackOrder => 0;

    public void OnPreprocessBuild(BuildReport report)
    {
        Debug.Log("Run prebuild editmode tests");

        ResultCollector result = new ResultCollector();

        TestRunnerApi api = ScriptableObject.CreateInstance<TestRunnerApi>();
        api.RegisterCallbacks(result);

        api.Execute(new ExecutionSettings
        {
            runSynchronously = true,
            filters = new[]{ new Filter{
                testMode = TestMode.EditMode
            }}
        });

        if (result.Result.FailCount > 0)
            throw new BuildFailedException($"{result.Result.FailCount} tests failed");

        Debug.Log($"Tests passed: {result.Result.PassCount}");
    }
}
