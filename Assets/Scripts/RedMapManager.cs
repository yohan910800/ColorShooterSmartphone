using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MankindGames;

public class RedMapManager : MonoBehaviour {

    public GameObject[] prefabs;
    GameObject prefab;

    public GameManager gm;
    public Button spawnBtn;
    public Button platoonSpawnBtn;
    public Dropdown dropdown;
    public PlatoonManager platoonManager;
    

    public int squadCount;
    [Range(1,10)]
    public int squadMemberRows;
    [Range(1,10)]
    public int squadMemberCols;
    public Colors[] colors;
    public int[] ratios;
    public float fireRate=1;

    void Start() {
        dropdown.options.Clear();
        foreach (var pr in prefabs){
            dropdown.options.Add(new Dropdown.OptionData(pr.name));
        }
        dropdown.RefreshShownValue();
        prefab=prefabs[0];
    }

    void Update() {
        
    }

    public void SelectPrefab(int i){
        prefab = prefabs[i];
    }

    public void Spawn(){
        Vector3 pos = new Vector3(0,-5,0);
        ICharacter enemy = Instantiate(prefab, pos, Quaternion.identity).GetComponent<ICharacter>();
        enemy.Init();
        gm.AddEnemy(enemy);
        spawnBtn.interactable = false;
        platoonSpawnBtn.interactable = false;
        enemy.OnDeathEvent += OnEnemyDeath;
    }

    public void OnEnemyDeath(ICharacter character){
        character.OnDeathEvent -= OnEnemyDeath;
        spawnBtn.interactable = true;
        platoonSpawnBtn.interactable = true;
    }

    public void SpawnPlatoon(){
        if(squadCount>colors.Length) squadCount=colors.Length;
        Vector3[] positions = new Vector3[squadCount];
        float squadWidth = (squadMemberCols+1)*1.1f;
        float separation = squadWidth+2;
        float totalWidth = separation*(squadCount-1);
        for(int i=0; i<squadCount; i++){
            float x = i*separation - totalWidth/2;
            float y = 5 + squadMemberRows/2;
            positions[i] = new Vector3(x,y,0);
        }
        Dictionary<Colors,int> colorRates = new Dictionary<Colors, int>();
        for(int i=0; i<colors.Length; i ++){
            colorRates.Add(colors[i],ratios[i]);
        }
        platoonManager.SpawnPlatoon("SquadMemberV1_Base", positions, squadMemberRows, squadMemberCols, colorRates, fireRate);
        spawnBtn.interactable = false;
        platoonSpawnBtn.interactable = false;
    }

    public void Reset(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
