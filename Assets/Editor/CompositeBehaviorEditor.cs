using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CompositeBehavior))]
public class CompositeBehaviorEditor : Editor
{
    private FlockBehavior adding;

    // new method for removing weights 
    private float[] Remove(int index, float[] old)
    {
        // Remove this behaviour
        var cb = new float[old.Length - 1];

        for (int y = 0, x = 0; y < old.Length; y++)
        {
            if (y != index)
            {
                cb[x] = old[y];
                x++;
            }
        }

        return cb;
    }


    private FlockBehavior[] Remove(int index, FlockBehavior[] old)
    {
        // Remove this behaviour
        var cb = new FlockBehavior[old.Length - 1];

        for (int y = 0, x = 0; y < old.Length; y++)
        {
            if (y != index)
            {
                cb[x] = old[y];
                x++;
            }
        }

        return cb;
    }

    public override void OnInspectorGUI()
    {
        // Setup
        var cb = (CompositeBehavior)target;
        EditorGUILayout.BeginHorizontal();

        // Draw
        if (cb.behaviors == null || cb.behaviors.Length == 0)
        {
            EditorGUILayout.HelpBox("No behaviors attached.", MessageType.Warning);
            EditorGUILayout.EndHorizontal();
        }
        else
        {
            EditorGUILayout.LabelField("behaviors");
            EditorGUILayout.LabelField("Weights");

            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < cb.behaviors.Length; i++)
            {
                // Draw index
                EditorGUILayout.BeginHorizontal();

                if (GUILayout.Button("Remove") || cb.behaviors[i] == null)
                {
                    // Remove this behaviour
                    cb.behaviors = Remove(i, cb.behaviors);
                    cb.weights = Remove(i, cb.weights);
                    break;
                }

                cb.behaviors[i] = (FlockBehavior)EditorGUILayout.ObjectField(cb.behaviors[i], typeof(FlockBehavior), false);
                EditorGUILayout.Space(30);
                cb.weights[i] = EditorGUILayout.Slider(cb.weights[i], 0, 10);

                EditorGUILayout.EndHorizontal();
            }
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Add behaviour...");
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        adding = (FlockBehavior)EditorGUILayout.ObjectField(adding, typeof(FlockBehavior), false);

        if (adding != null)
        {
            // add this item to the array
            var oldbehaviors = cb.behaviors;
            cb.behaviors = new FlockBehavior[oldbehaviors.Length + 1];
            var oldWeights = cb.weights;
            cb.weights = new float[oldWeights.Length + 1];

            for (int i = 0; i < oldbehaviors.Length; i++)
            {
                cb.behaviors[i] = oldbehaviors[i];
                cb.weights[i] = oldWeights[i];
            }

            cb.behaviors[oldbehaviors.Length] = adding;
            cb.weights[oldWeights.Length] = 1;

            adding = null;
        }
    }
}
