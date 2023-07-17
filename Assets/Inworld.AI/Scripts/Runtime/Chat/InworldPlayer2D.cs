/*************************************************************************************************
* Copyright 2022 Theai, Inc. (DBA Inworld)
*
* Use of this source code is governed by the Inworld.ai Software Development Kit License Agreement
* that can be found in the LICENSE.md file or at https://www.inworld.ai/sdk-license
*************************************************************************************************/
using Inworld.Packets;
using Inworld.Sample.UI;
using Inworld.Util;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
namespace Inworld.Sample
{
    public class InworldPlayer2D : MonoBehaviour
    {
        #region Inspector Variables
        [SerializeField] protected GameObject m_GlobalChatCanvas;
        [SerializeField] RectTransform m_ContentRT;
        [SerializeField] TMP_InputField m_InputField;
        [SerializeField] GameObject m_TextDisplay;
        #endregion

        #region Private Variables
        readonly Dictionary<string, InworldCharacter> m_Characters = new Dictionary<string, InworldCharacter>();
        #endregion

        #region Public Function
        /// <summary>
        ///     UI Functions. Called by button "Send" clicked or Keycode.Return clicked.
        /// </summary>
        public void SendText()
        {
            if (string.IsNullOrEmpty(m_InputField.text))
                return;
            if (!InworldController.Instance.CurrentCharacter)
            {
                InworldAI.LogError("No Character is interacting.");
                return;
            }
            InworldController.Instance.CurrentCharacter.SendText(m_InputField.text);
            m_InputField.text = null;
        }

        public void RegisterCharacter(InworldCharacter character) => character.InteractionEvent.RemoveListener(OnInteractionStatus);
        #endregion

        #region Monobehavior Functions
        void Start()
        {
            InworldController.Instance.OnStateChanged += OnControllerStatusChanged;
        }

        void Update()
        {
            UpdateSendText();
        }
        #endregion

        #region Callbacks
        protected void OnControllerStatusChanged(ControllerStates states)
        {
            if (states != ControllerStates.Connected)
                return;
            ClearHistoryLog();
            foreach (InworldCharacter iwChar in InworldController.Characters)
            {
                m_Characters[iwChar.ID] = iwChar;
                
            }
        }

        void OnInteractionStatus(InteractionStatus status, List<HistoryItem> historyItems)
        {
            if (status != InteractionStatus.HistoryChanged)
                return;
            if (m_TextDisplay)
                RefreshText(historyItems);
        }
        #endregion

        #region Private Functions
        protected void UpdateSendText()
        {
            if (!m_GlobalChatCanvas.activeSelf)
                return;
            if (!Input.GetKeyUp(KeyCode.Return) && !Input.GetKeyUp(KeyCode.KeypadEnter))
                return;
            SendText();
        }

        void RefreshText(List<HistoryItem> historyItems)
        {
            string displayText = "";
            foreach (HistoryItem item in historyItems)
            {
                if (item.Event is TextEvent textEvent)
                {
                    string characterName = "";
                    if (item.Event.Routing.Source.IsPlayer())
                    {
                        characterName = "<b>You:</b> ";
                    }
                    else if (item.Event.Routing.Source.IsAgent())
                    {
                        if (m_Characters.ContainsKey(item.Event.Routing.Source.Id))
                        {
                            InworldCharacter source = m_Characters[item.Event.Routing.Source.Id];
                            characterName = $"<b>{source.CharacterName}:</b> ";
                        }
                    }

                    displayText += $"<align=left>{characterName}{textEvent.Text}</align>\n";
                }
                if (item.Event is ActionEvent actionEvent)
                    displayText += "<align=left><i>" + actionEvent.Content + "</i></align>\n";
            }

            if (m_TextDisplay != null)
            {
                TextMeshProUGUI textMeshPro = m_TextDisplay.GetComponent<TextMeshProUGUI>();
                if (textMeshPro != null)
                    textMeshPro.text = displayText;
            }
        }

        void ClearHistoryLog()
        {
            if (m_TextDisplay != null)
            {
                TextMeshProUGUI textMeshPro = m_TextDisplay.GetComponent<TextMeshProUGUI>();
                if (textMeshPro != null)
                    textMeshPro.text = "";
            }
            m_Characters.Clear();
        }
        #endregion
    }
}