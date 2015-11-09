using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectObject : MonoBehaviour
{
    private bool m_IsPieceSelected;
    private Piece m_SelectedPiece;
    private Tile m_PieceTile;
    private bool m_IsTap;

    BoardManager board;
    // Use this for initialization
    void Start()
    {
        board = FindObjectOfType<BoardManager>();
        m_IsTap = false;
    }

    // Update is called once per frame
    void Update()
    {
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
                                if (objHit.tag == "piece" && !board.Moved)
                                {
                                    if (board.MovedPiece != objHit.GetComponent<Piece>())
                                    {
                                        //Find piece selected
                                        board.currentPiece = objHit.GetComponent<Piece>();
                                        m_IsPieceSelected = true;
                                        m_SelectedPiece = objHit.GetComponent<Piece>();
                                        m_PieceTile = m_SelectedPiece.Tile;
                                        //Dehighlight tiles
                                        board.RestoreAllTiles();
                                        //Highlight possible tiles
                                        board.FindMovementOptions();
                                        Debug.Log("hit piece");
                                        Debug.Log(objHit.name);
                                    }

                                }
                                else if (objHit.tag == "tile" && !board.Moved)
                                {
                                    Tile t = objHit.GetComponent<Tile>();
                                    Debug.Log("hit tile");
                                    if (m_IsPieceSelected)
                                    {
                                        Debug.Log("piece is selected");
                                        if (t.isHighlighted)
                                        {
                                            Debug.Log("asking to mvoe");
                                            m_SelectedPiece.MoveOnPathToTile(t, .5f);
                                            m_PieceTile.Piece = null;
                                            t.Piece = m_SelectedPiece;
                                            board.RegisterNewMove(m_PieceTile, t);
                                            board.RestoreAllTiles();
                                            //board.ChangeTurn();
                                        }

                                    }
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
                GameObject objHit = hit.transform.parent.gameObject;
                if (objHit.tag == "piece" && !board.Moved)
                {
                    if (board.MovedPiece != objHit.GetComponent<Piece>())
                    {
                        board.currentPiece = objHit.GetComponent<Piece>();
                        m_IsPieceSelected = true;
                        m_SelectedPiece = objHit.GetComponent<Piece>();
                        m_PieceTile = m_SelectedPiece.Tile;
                        board.RestoreAllTiles();
                        board.FindMovementOptions();
                        //Debug.Log("hit piece");
                        Debug.Log(objHit.gameObject.name);
                    }


                }
                else if (objHit.tag == "tile" && !board.Moved)
                {
                    Tile t = objHit.GetComponent<Tile>();
                    //Debug.Log("hit tile");
                    if (m_IsPieceSelected)
                    {
                        //Debug.Log("piece is selected");
                        if (t.isHighlighted)
                        {
                            //Debug.Log("asking to mvoe");
                            m_SelectedPiece.MoveOnPathToTile(t, .5f);
                            m_PieceTile.Piece = null;
                            t.Piece = m_SelectedPiece;
                            board.RestoreAllTiles();
                            board.RegisterNewMove(m_PieceTile, t);
                            //board.ChangeTurn();
                        }

                    }
                }
            }
        }

#endif

        #endregion
    }
}
