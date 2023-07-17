/*************************************************************************************************
* Copyright 2022 Theai, Inc. (DBA Inworld)
*
* Use of this source code is governed by the Inworld.ai Software Development Kit License Agreement
* that can be found in the LICENSE.md file or at https://www.inworld.ai/sdk-license
*************************************************************************************************/
using Inworld.Packets;
using Inworld.Util;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Inworld.Sample.UI
{
    /// <summary>
    ///     This class is used to show/hide the Inworld Characters' floating text bubble.
    /// </summary>
    public class ChatPanel3D : MonoBehaviour
    {
        #region Inspector Variables
        [SerializeField] RectTransform m_PanelAnchor;
        [SerializeField] InworldCharacter m_Owner;
        [Header("UI Expressions")]
        [SerializeField] GameObject m_Dots;
        [SerializeField] Image m_EmoIcon;
        Color m_IconColor = Color.white;
        #endregion

        #region Private Variables
        readonly Dictionary<string, ChatBubble> m_Bubbles = new Dictionary<string, ChatBubble>(12);
        #endregion

        #region MonoBehaviour Functions
        void Update()
        {
            m_IconColor.a *= 0.99f;
            if (m_EmoIcon)
                m_EmoIcon.color = m_IconColor;
        }

        void OnEnable()
        {
            if (m_EmoIcon)
                m_IconColor = m_EmoIcon.color;
            m_IconColor.a = 1;
            ClearHistoryLog();
            m_Owner.InteractionEvent.AddListener(OnInteractionStatus);
        }
        #endregion

        #region Public Functions
        internal void ProcessEmotion(FacialAnimation face)
        {
            m_IconColor.a = 1;
            if (m_EmoIcon)
                m_EmoIcon.sprite = face.icon;
        }
        #endregion

        #region Callbacks
        void OnInteractionStatus(InteractionStatus status, List<HistoryItem> historyItems)
        {
            if (status != InteractionStatus.HistoryChanged)
                return;
            RefreshBubbles(historyItems);
        }
        #endregion

        #region Private Functions
        void RefreshBubbles(List<HistoryItem> historyItems)
        {
            foreach (HistoryItem item in historyItems)
            {
                if (item.Event is TextEvent textEvent)
                    Debug.Log($"Character: {item.Event.Routing.Source.Id}, Text: {textEvent.Text}");
                if (item.Event is ActionEvent actionEvent)
                    Debug.Log($"Character: {item.Event.Routing.Source.Id}, Action: {actionEvent.Content}");
            }
        }

        void ClearHistoryLog()
        {
            m_Bubbles.Clear();
        }
        #endregion
    }
}