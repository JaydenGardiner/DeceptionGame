using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SecretNumberController : MonoBehaviour
{
    private bool m_IsPieceSelected;
    private bool m_IsTap;

    public CreateGameData Data;
    public Button cButton;

    private int m_curNumber;

    private List<Piece> pieces;

    public void ConfirmButton()
    {
        if (m_IsPieceSelected)
        {
            Data.SecretNumber = m_curNumber;
        }
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
            piece.AddComponent<MeshCollider>();
            pieces.Add(piece.GetComponent<Piece>());
        }
        
    }


    // Update is called once per frame
    void Update()
    {
        if (m_IsPieceSelected)
        {
            cButton.interactable = false;
        }
        else
        {
            cButton.interactable = true;
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
                                        p.IsSecret = false;
                                        p.HighlightPiece();
                                    }
                                    //Find piece selected
                                    m_IsPieceSelected = true;
                                    Piece m_SelectedPiece = objHit.GetComponent<Piece>();
                                    m_SelectedPiece.IsSecret = true;
                                    
                                    m_curNumber = m_SelectedPiece.PieceNumber;
                                    Debug.Log("hit piece");
                                    Debug.Log(objHit.name);

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
                    //Find piece selected
                    m_IsPieceSelected = true;
                    Piece m_SelectedPiece = objHit.GetComponent<Piece>();
                    m_SelectedPiece.IsSecret = true;
                    m_SelectedPiece.HighlightPiece();
                    m_curNumber = m_SelectedPiece.PieceNumber;
                    Debug.Log("hit piece");
                    Debug.Log(objHit.name);

                }
            }
        }

#endif

        #endregion
    }
}