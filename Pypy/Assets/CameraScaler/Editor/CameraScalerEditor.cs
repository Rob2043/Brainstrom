using UnityEditor;
using UnityEngine;

namespace EmeraldPowder.CameraScaler
{
    [CustomEditor(typeof(CameraScaler))]
    [CanEditMultipleObjects]
    public class CameraScalerEditor : Editor
    {
        private SerializedProperty ReferenceResolution;
        private SerializedProperty Mode;
        private SerializedProperty MatchWidthOrHeight;

        private void OnEnable()
        {
            ReferenceResolution = serializedObject.FindProperty("ReferenceResolution");
            Mode = serializedObject.FindProperty("Mode");
            MatchWidthOrHeight = serializedObject.FindProperty("MatchWidthOrHeight");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(ReferenceResolution);
            EditorGUILayout.PropertyField(Mode);

            if (!Mode.hasMultipleDifferentValues)
            {
                CameraScaler.WorkingMode workingMode = (CameraScaler.WorkingMode) Mode.enumValueIndex;

                if (workingMode == CameraScaler.WorkingMode.ConstantHeight)
                {
                    const string msg = "This mode works just like a normal camera, so you might just remove CameraScaler component";
                    EditorGUILayout.HelpBox(msg, MessageType.Info);
                }
                else if (workingMode == CameraScaler.WorkingMode.MatchWidthOrHeight)
                {
                    Rect r = EditorGUILayout.GetControlRect(true, EditorGUIUtility.singleLineHeight + 12);
                    DualLabeledSlider(r, MatchWidthOrHeight, "Match", "Width", "Height");
                }
            }

            serializedObject.ApplyModifiedProperties();
        }

        private static void DualLabeledSlider(Rect position, SerializedProperty property, string mainLabel,
            string labelLeft, string labelRight)
        {
            position.height = EditorGUIUtility.singleLineHeight;
            Rect pos = position;

            position.y += 12;
            position.xMin += EditorGUIUtility.labelWidth;
            position.xMax -= EditorGUIUtility.fieldWidth;

            GUI.Label(position, labelLeft, new GUIStyle(EditorStyles.label));
            GUI.Label(position, labelRight, new GUIStyle(EditorStyles.label) {alignment = TextAnchor.MiddleRight});

            EditorGUI.Slider(pos, property, 0, 1, mainLabel);
        }
    }
}