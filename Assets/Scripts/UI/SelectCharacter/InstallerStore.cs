using UnityEngine;

public class InstallerStore : MonoBehaviour
{
    [SerializeField] CharacterList _listCharacter;

    [SerializeField] ManagerSloct _managerSloct;
    // Start is called before the first frame update

    private void Start() {
        SceneSetup();
    }
    public void SceneSetup()
    {
        _managerSloct.Setup();
    }
}
