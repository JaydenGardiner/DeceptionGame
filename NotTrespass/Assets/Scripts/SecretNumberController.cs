using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SecretNumberController : MonoBehaviour
{
    private bool m_IsPieceSelected;
    private bool m_IsTap;
    private bool m_IsSelectionChanged;

    public Button cButton;
    public Text NumberText;

    private int m_curNumber;

    private List<Piece> pieces;

    public void ConfirmButton()
    {
        if (m_IsPieceSelected)
        {
			SharedSceneData.GameToLoad.SetSecretNumber(m_curNumber);
            print("loading next scene");
			SharedSceneData.API.CreateNewGame(SharedSceneData.GameToLoad);
            Application.LoadLevel("GameScene");
        }
    }

    public void CancelButton()
    {
        Application.LoadLevel("MenuScreen");
    }


    // Use this for initialization
    void Start()
    {
        m_IsPieceSelected = false;
        m_IsTap = false;
        pieces = new List<Piece>();
        GameObject[] objs = GameObject.FindGameObjectsWithTag("piece");
        foreach(GameObject piece in objs)
        {
            //piece.AddComponent<MeshCollider>();
            pieces.Add(piece.GetComponent<Piece>());
        }
        m_IsSelectionChanged = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (m_IsPieceSelected)
        {
            cButton.interactable = true;
        }
        else
        {
            cButton.interactable = false;
        }
        if (m_IsSelectionChanged)
        {
            foreach (Piece p in pieces)
            {
                if (p != null)
                {
                    p.HighlightPiece(0);
                }
            }
            m_IsSelectionChanged = false;
        }
        // || Input.GetMouseButtonDown(0)
        if (Input.touchCount > 0)
        {
            Touch curTouch = Input.GetTouch(0);

            switch (curTouch.phase)
            {
                case TouchPhase.Began:
                    m_IsTap = true;
                    break;
                case TouchPhase.Moved:
                    m_IsTap = false;
                    break;
                case TouchPhase.Ended:
                    if (m_IsTap)
                    {
                        if (curTouch.tapCount == 1)
                        {
                            RaycastHit hit;
                            Ray worldPos = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                            if (Physics.Raycast(worldPos, out hit, Mathf.Infinity))
                            {
                                
                                GameObject objHit = hit.transform.parent.gameObject;
                                if (objHit.tag == "piece")
                                {
                                    //remove highlight
                                    foreach (Piece p in pieces)
                                    {
                                        if (p != null)
                                        {
                                            p.IsSecret = false;
                                        }

                                    }
                                    //Find piece selected
                                    m_IsPieceSelected = true;
                                    Piece m_SelectedPiece = objHit.GetComponent<Piece>();
                                    m_SelectedPiece.IsSecret = true;
                                    m_curNumber = m_SelectedPiece.PieceNumber;
                                    NumberText.text = "Choose your secret piece: " + m_curNumber;
                                    m_IsSelectionChanged = true;
                                }

                            }
                        }
                    }


                    break;
            }
        }

        #region debug
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray worldPos = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(worldPos, out hit, Mathf.Infinity))
            {
                print("hit");
                GameObject objHit = hit.transform.parent.gameObject;
                if (objHit.tag == "piece")
                {
                    //remove highlight
                    foreach (Piece p in pieces)
                    {
                        if (p != null)
                        {
                            p.IsSecret = false;
                        }
                        
                    }
                    //Find piece selected
                    m_IsPieceSelected = true;
                    Piece m_SelectedPiece = objHit.GetComponent<Piece>();
                    m_SelectedPiece.IsSecret = true;
                    m_SelectedPiece.HighlightPiece(0);
                    m_curNumber = m_SelectedPiece.PieceNumber;
                    NumberText.text = "Choose your secret piece: " + m_curNumber;
                    m_IsSelectionChanged = true;
                }
            }
        }

#endif

        #endregion
    }
}