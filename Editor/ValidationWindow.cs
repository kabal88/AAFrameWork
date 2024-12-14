using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AAFramework.Interfaces;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

public partial class ValidationWindow : OdinEditorWindow
{
    [SerializeField, ShowIf(nameof(hasValidationErrors)), BoxGroup("Problems")]
    private List<MonoBehaviour> monoValidationErrors = new();
    [SerializeField, ShowIf(nameof(hasValidationErrors)), BoxGroup("Problems")]
    private List<ScriptableObject> soValidationErrors = new();
    
    
    private bool hasValidationErrors;
    
    [MenuItem("Game/Validation Window")]
    public static void GetWindow()
    {
        GetWindow<ValidationWindow>();
    }
    
    [Button(ButtonSizes.Medium)]
    private void CheckValidation()
    {
        hasValidationErrors = false;
        monoValidationErrors.Clear();
        soValidationErrors.Clear();
        var objectsOnScene = FindObjectsOfType<MonoBehaviour>(true);
        var success = true;
        var monoCount = 0;

        foreach (var o in objectsOnScene)
        {
            if (o is IValidatable validatable)
            {
                monoCount++;
                if (!validatable.Validate())
                {
                    hasValidationErrors = true;
                    success = false;
                    monoValidationErrors.Add(o);
                }
            }
        }

        var provider = new SOProvider<ScriptableObject>();
        
       var soInProject = provider.GetCollection().ToList();
        
        var soCount = 0;
        
        foreach (var so in soInProject)
        {
            if (so is IValidatable validatable)
            {
                soCount++;
                if (!validatable.Validate())
                {
                    hasValidationErrors = true;
                    success = false;
                    soValidationErrors.Add(so);
                }
            }
        }

        if (success)
        {
            Debug.Log($"Validation successful. Checked as IValidatable {monoCount}({objectsOnScene.Length}) MonoBehaviour and {soCount}({soInProject.Count}) scriptable objects.");
        }
        else
        {
            Debug.LogError($"Validation failed. Checked as IValidatable {monoCount}({objectsOnScene.Length}) MonoBehaviour and {soCount}({soInProject.Count}) scriptable objects.");
        }
    }
}
