using UnityEngine;

namespace nitou.BachProcessor{

    /// <summary>
    /// �o�b�`�����Ώۂ̃R���|�[�l���g���N���X�D
    /// Register this class with a class that inherits from <see cref="SystemBase{TComponent, TSystem}"/> for usage.
    /// </summary>
    public abstract class ComponentBase : MonoBehaviour, IComponentIndex{

        protected int Index { get; private set; } = -1;
        protected bool IsRegistered => Index != -1;


        /// ----------------------------------------------------------------------------
        // Interface

        /// <summary>
        /// Index of the component during batch processing.
        /// </summary>
        int IComponentIndex.Index{
            get => Index;
            set => Index = value;
        }

        /// <summary>
        /// �V�X�e���֓o�^�ς݂��ǂ���
        /// </summary>
        bool IComponentIndex.IsRegistered => IsRegistered;
    }
}