using Player;
using UnityEngine;

namespace Environment
{
    internal sealed class Ocean : MonoBehaviour
    {
        private Renderer m_WaterRenderer;

        [SerializeField]
        private Color waterColor;
        [SerializeField]
        private Texture waterTexture;
        [SerializeField]
        private Vector2 moveDirection;

        private readonly Vector2 m_WaterTile = new Vector2(0.1f, 0.1f);
        private const float TextureVisibility = 0.3f;

        private MaterialPropertyBlock m_MaterialPropertyBlock;

        private PlayerStats PlayerStats => GameManager.Instance.playerStats;

        private void Start()
        {
            m_WaterRenderer = GetComponent<Renderer>();
            m_MaterialPropertyBlock = new MaterialPropertyBlock();
            SetUpPropertyBlock(m_MaterialPropertyBlock);
        }

        // Update property block every frame so we can update the movement
        //
        private void Update()
        {
            m_WaterRenderer.SetPropertyBlock(m_MaterialPropertyBlock);
        }

        /// <summary>
        /// Set properties of a material block so we can apply numerous values to one object
        /// </summary>
        private void SetUpPropertyBlock(MaterialPropertyBlock propertyBlock)
        {
            propertyBlock.SetColor("_WaterColor", waterColor);
            propertyBlock.SetVector("_Tiling", m_WaterTile);
            propertyBlock.SetVector("_MoveDirection", new Vector4(moveDirection.x, 0f, moveDirection.y, 0f));
            propertyBlock.SetTexture("_WaterTex", waterTexture);
            propertyBlock.SetFloat("_TextureVisibility", TextureVisibility);
        }
    }
}
