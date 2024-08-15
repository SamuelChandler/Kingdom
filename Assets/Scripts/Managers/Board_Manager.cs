using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class Board_Manager : MonoBehaviour, IDataPersistance
{
    public static Board_Manager instance;

    [SerializeField] public ScriptableMap _map;

    [SerializeField] private Transform _camera;

    [SerializeField] private int Tilesize;

    private Dictionary<Vector2, Tile> _tiles;

    public BaseEnemy Enemy_Prefab;
    public BaseHero Hero_Prefab;
    public Leader Hero_Leader_Prefab;


    public EnemyBoss Boss_Prefab;

    public AllyStructure _allyS;
    public EnemyStructure _enemyS;
    public NeutralStructure _neutralS;

    public List<BaseHero> _heroes = new List<BaseHero>();
    public List<BaseEnemy> _enemies = new List<BaseEnemy>();
    public List<Structure> _AllyStructures = new List<Structure>();
    public List<Structure> _EnemyStructures = new List<Structure>();
    public List<Structure> _NeutralStructures = new List<Structure>();

    public int allyAttackBuff;
    public int enemyAttackBuff;


    private void Awake()
    {
        instance = this;

        //clear field buffs
        allyAttackBuff = 0;
        enemyAttackBuff = 0;
    }

    //generates grid for the game , creates tiles 
    public void generateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        var log = 0;
        var j = 0;
        for(int i = 0; i < _map.MapTiles.Length; i++){

            var id = _map.MapTiles.ElementAt(i);

            if(id != '\n')
            {
                var spawnedTile = Instantiate(_map.GetTile(id), new Vector3(Tilesize * log,Tilesize *-j), Quaternion.identity);
                spawnedTile.name = $"Tile {log} {j}";

                spawnedTile.Init(log, j);

                _tiles[new Vector2(log, j)] = spawnedTile;

                log++;
            }
            else
            {
                j++;
                log = 0;
               
            }
        }

        _camera.transform.position = new Vector3(Tilesize*((float)_map.width/ 2- 0.5f), -Tilesize * ((float)_map.height /2 - 0.5f),-1);

        Game_Manager.instance.ChangeState(GameState.SpawnHero);
    }

    public void SpawnLeader(ScriptableUnit leader){
        Tile destTile = GetTileAtPosition(_map._leader.loc);

        if (destTile == null)
        {
            Debug.Log("Leader: Dest Tile does not exist");
            return;
        }
        if (leader == null)
        {
            Debug.Log("Leader: Sciptable Unit not Defined");
            return;
        }


        Hero_Leader_Prefab.unit = leader;
        var summonded_Hero = Instantiate(Hero_Leader_Prefab);
        destTile.setUnit(summonded_Hero);

        _heroes.Add(summonded_Hero);
    }

    public void SpawnBoss(){
        Tile destTile = GetTileAtPosition(_map._boss.loc);
        ScriptableUnit unit = _map._boss.enemy;

        if (destTile == null)
        {
            Debug.Log("Boss: Dest Tile does not exist");
            return;
        }
        if (unit == null)
        {
            Debug.Log("Boss: Sciptable Unit not Defined");
            return;
        }


        Boss_Prefab.unit = unit;
        var summonded_Enemy = Instantiate(Boss_Prefab);
        destTile.setUnit(summonded_Enemy);

        _enemies.Add(summonded_Enemy);
    }
    //returns a tile for a givem v2 positon
    public Tile GetTileAtPosition(Vector2 position)
    {
        if(_tiles.TryGetValue(position, out Tile tile))
        {
            return tile;
        }

        return null;
    }

    public int GetDistanceBetweenTiles(Tile src,Tile dst){
         
        //determine the change in the x and y axis
        int x_change = Mathf.Abs(src.x - dst.x);
        int y_change = Mathf.Abs(src.y - dst.y);

        int total_change  = x_change + y_change;

        return total_change;

    }
    
    public Tile GetTileAtPosition(Tile src, Vector2 offset)
    {

        if(src == null){
            return null;
        }

        Vector2 position = new Vector2(src.x + offset.x, src.y + offset.y);

        if(_tiles.TryGetValue(position, out Tile tile))
        {
            return tile;
        }

        return null;
    }

    //returns a Unit for a given v2 position
    public BaseUnit GetUnitAtPosition(Vector2 position)
    {
        //checks if tile exists
        if (_tiles.TryGetValue(position,out Tile tile))
        {
            if (tile.OccupiedUnit != null)
            {
                return tile.OccupiedUnit;
            }
        }

        return null;
    }

    //Attempts to summon a unit to the destTile 
    public void SummonUnit(Tile destTile,BaseUnit unit)
    {
        if(unit.unit.Faction == Faction.Hero)
        {
            //do not summon a ally unit that cannot be played
            if (!Game_Manager.instance.CanBePlayed(unit,destTile)) { return; }

            //pay cost relating to Unit
            Game_Manager.instance.DecreaseCurrentInsperation(unit.unit.inspirationCost);

            //remove unit from hand
            Menu_Manager.instance.CurrentSelectedSelector.ClearCard();

            //create and set unit to tile
            var summonded_Hero = (BaseHero)Instantiate(unit);
            destTile.setUnit(summonded_Hero);

            _heroes.Add(summonded_Hero);

            if(summonded_Hero.unit.OnPlay != null){
                summonded_Hero.unit.OnPlay.ActivateEffect(summonded_Hero);
            }

            ClearBoardIndicators();
            Unit_Manager.instance.SelectedHero = null;
            Menu_Manager.instance.CurrentSelectedSelector = null;
        }
        
    }

    // summoning without the decrease of inspiration and UI logic
    public void SpawnUnit(Tile destTile, ScriptableUnit unit){
        if (destTile == null)
        {
            Debug.Log("Dest Tile does not exist");
            return;
        }
        if (unit == null)
        {
            Debug.Log("Sciptable Unit not Defined");
            return;
        }

        if (unit.Faction == Faction.Hero)
        {
            Hero_Prefab.unit = unit;

            //create and set unit to tile
            var summonded_Hero = Instantiate(Hero_Prefab);
            destTile.setUnit(summonded_Hero);

            _heroes.Add(summonded_Hero);
        }

        else if (unit.Faction == Faction.Enemy)
        {
            Enemy_Prefab.unit = unit;

            //create and set unit to tile
            BaseEnemy summonded_Enemy = Instantiate(Enemy_Prefab);
            destTile.setUnit(summonded_Enemy);

            _enemies.Add(summonded_Enemy);

        }
    }

    //Attempts to summon a unit to a dest Tile based on a scriptable unit
    public void SummonUnit(Tile destTile, ScriptableUnit unit)
    {

        if (destTile == null)
        {
            Debug.Log("Dest Tile does not exist");
            return;
        }
        if (unit == null)
        {
            Debug.Log("Sciptable Unit not Defined");
            return;
        }

        if (unit.Faction == Faction.Hero)
        {
            Hero_Prefab.unit = unit;
            //do not summon a ally unit that cannot be played
            if (!Game_Manager.instance.CanBePlayed(Hero_Prefab,destTile)) { return; }

            //pay cost relating to Unit
            Game_Manager.instance.DecreaseCurrentInsperation(Hero_Prefab.unit.inspirationCost);
            
            //remove unit from hand
            Menu_Manager.instance.CurrentSelectedSelector.ClearCard();

            //create and set unit to tile
            var summonded_Hero = Instantiate(Hero_Prefab);
            destTile.setUnit(summonded_Hero);

            _heroes.Add(summonded_Hero);

            if(summonded_Hero.unit.OnPlay != null){
                summonded_Hero.unit.OnPlay.ActivateEffect(summonded_Hero);
            }

            ClearBoardIndicators();
            Unit_Manager.instance.SelectedHero = null;
            Menu_Manager.instance.CurrentSelectedSelector = null;
        }

        else if (unit.Faction == Faction.Enemy)
        {
            Enemy_Prefab.unit = unit;

            //create and set unit to tile
            BaseEnemy summonded_Enemy = Instantiate(Enemy_Prefab);
            destTile.setUnit(summonded_Enemy);

            _enemies.Add(summonded_Enemy);

        }
    }

    public void SummonStructure(StructureAndPoint s){
        //input val
        if (s.loc == null)
        {
            Debug.Log("Dest Tile does not exist");
            return;
        }
        if (s == null)
        {
            Debug.Log("Sciptable Unit not Defined");
            return;
        }

        if(s.structure.Faction == Faction.Hero){
            _allyS._structure = s.structure;

            if (!Game_Manager.instance.CanBePlayed(_allyS)) { return; }
            //pay cost relating to Unit
            Game_Manager.instance.DecreaseCurrentInsperation(_allyS._structure.inspirationCost);

            //create and set unit to tile
            var summonded_Structure = Instantiate(_allyS);

            if((s.structure.width == 0|| s.structure.width ==1) && (s.structure.height == 0||s.structure.height == 1)){
                summonded_Structure.OccupiedTiles = new Tile[1,1];
                summonded_Structure.OccupiedTiles[0,0] = GetTileAtPosition(s.loc).setStructure(summonded_Structure);;
                
                //remove unit from hand and clear related UI
                Menu_Manager.instance.CurrentSelectedSelector.ClearCard();
                Unit_Manager.instance.SelectedCardInHand = null;
                Menu_Manager.instance.CurrentSelectedSelector = null;
                
                _AllyStructures.Add(summonded_Structure);
                ClearBoardIndicators();

                //trigger on summon if needed 
                if(summonded_Structure._structure.OnSummon != null){
                    summonded_Structure._structure.OnSummon.ActivateEffect(summonded_Structure);
                }

                return;
            }

            //set to all tiles for the size of the structure
            summonded_Structure.OccupiedTiles = new Tile[s.structure.width,s.structure.height];
            
            for(int i = 0; i < s.structure.width;i++){
                for(int j =0; j < s.structure.height; j++){
                    summonded_Structure.OccupiedTiles[i,j] = GetTileAtPosition(new Vector2(s.loc.x+i, s.loc.y+j)).setStructure(summonded_Structure);
                }
            }

            //set postion based on the middle of oposite tiles 
            float x = summonded_Structure.OccupiedTiles[0,0].transform.position.x + ((summonded_Structure.OccupiedTiles[s.structure.width-1,s.structure.height-1].transform.position.x - summonded_Structure.OccupiedTiles[0,0].transform.position.x)/2);
            float y = summonded_Structure.OccupiedTiles[0,0].transform.position.y + ((summonded_Structure.OccupiedTiles[s.structure.width-1,s.structure.height-1].transform.position.y - summonded_Structure.OccupiedTiles[0,0].transform.position.y)/2);
            summonded_Structure.transform.position = new Vector3(x,y,0.0f);
  
            _AllyStructures.Add(summonded_Structure);

            //remove unit from hand and clear related UI
            Menu_Manager.instance.CurrentSelectedSelector.ClearCard();
            Unit_Manager.instance.SelectedCardInHand = null;
            Menu_Manager.instance.CurrentSelectedSelector = null;

            if(summonded_Structure._structure.OnSummon != null){
                    summonded_Structure._structure.OnSummon.ActivateEffect(summonded_Structure);
            }
            
            return;

            

        }
        else if(s.structure.Faction == Faction.Enemy){
            _enemyS._structure = s.structure;

            //create and set unit to tile
            var summonded_Structure = Instantiate(_enemyS);


            if((s.structure.width == 0|| s.structure.width ==1) && (s.structure.height == 0||s.structure.height == 1)){
                summonded_Structure.OccupiedTiles = new Tile[1,1];
                summonded_Structure.OccupiedTiles[0,0] = GetTileAtPosition(s.loc).setStructure(summonded_Structure);
                _EnemyStructures.Add(summonded_Structure);
                return;
            }

            //set to all tiles for the size of the structure
            summonded_Structure.OccupiedTiles = new Tile[s.structure.width,s.structure.height];

            for(int i = 0; i < s.structure.width;i++){
                for(int j =0; j < s.structure.height; j++){
                    summonded_Structure.OccupiedTiles[i,j] = GetTileAtPosition(new Vector2(s.loc.x+i, s.loc.y+j)).setStructure(summonded_Structure);
                }
            }

            

            //set postion based on the middle of oposite tiles 
            float x = summonded_Structure.OccupiedTiles[0,0].transform.position.x + ((summonded_Structure.OccupiedTiles[s.structure.width-1,s.structure.height-1].transform.position.x - summonded_Structure.OccupiedTiles[0,0].transform.position.x)/2);
            float y = summonded_Structure.OccupiedTiles[0,0].transform.position.y + ((summonded_Structure.OccupiedTiles[s.structure.width-1,s.structure.height-1].transform.position.y - summonded_Structure.OccupiedTiles[0,0].transform.position.y)/2);
            summonded_Structure.transform.position = new Vector3(x,y,0.0f);

            _EnemyStructures.Add(summonded_Structure);

            if(summonded_Structure._structure.OnSummon != null){
                    summonded_Structure._structure.OnSummon.ActivateEffect(summonded_Structure);
            }

            return;
        }
        else if(s.structure.Faction == Faction.Neutral){
            _neutralS._structure = s.structure;

            //create and set unit to tile
            var summonded_Structure = Instantiate(_neutralS);


            if((s.structure.width == 0|| s.structure.width ==1) && (s.structure.height == 0||s.structure.height == 1)){
                summonded_Structure.OccupiedTiles = new Tile[1,1];
                summonded_Structure.OccupiedTiles[0,0] = GetTileAtPosition(s.loc).setStructure(summonded_Structure);
                _NeutralStructures.Add(summonded_Structure);
                return;
            }

            //set to all tiles for the size of the structure
            summonded_Structure.OccupiedTiles = new Tile[s.structure.width,s.structure.height];

            for(int i = 0; i < s.structure.width;i++){
                for(int j =0; j < s.structure.height; j++){
                    summonded_Structure.OccupiedTiles[i,j] = GetTileAtPosition(new Vector2(s.loc.x+i, s.loc.y+j)).setStructure(summonded_Structure);
                }
            }

            //set postion based on the middle of oposite tiles 
            float x = summonded_Structure.OccupiedTiles[0,0].transform.position.x + ((summonded_Structure.OccupiedTiles[s.structure.width-1,s.structure.height-1].transform.position.x - summonded_Structure.OccupiedTiles[0,0].transform.position.x)/2);
            float y = summonded_Structure.OccupiedTiles[0,0].transform.position.y + ((summonded_Structure.OccupiedTiles[s.structure.width-1,s.structure.height-1].transform.position.y - summonded_Structure.OccupiedTiles[0,0].transform.position.y)/2);
            summonded_Structure.transform.position = new Vector3(x,y,0.0f);

            _NeutralStructures.Add(summonded_Structure);

            if(summonded_Structure._structure.OnSummon != null){
                summonded_Structure._structure.OnSummon.ActivateEffect(summonded_Structure);
            }
            return;
        }

    }
    //used to spawn all the enemies for the enemy list in the map scriptable object
    public void SpawnMapEnemies()
    {
        Debug.Log("Spawning Enemies");
        foreach (EnemyAndPoint item in _map.enemies)
        {
            SummonUnit(GetTileAtPosition(item.loc), item.enemy);
        }
    }

    public void SpawnMapStructures()
    {
        foreach (StructureAndPoint item in _map.structures)
        {
            SummonStructure(item);
        }
    }

    //used to move a unit from one tile to another 
    public void MoveUnit(Tile destTile, BaseUnit unit)
    {
        if(unit == null || destTile == null || !unit.isAbleToMove) return; //do nothing if the unit or tile does not exist. 

        Tile sourceTile = unit.OccupiedTile;

        //determine the change in the x and y axis
        int x_change = Mathf.Abs(sourceTile.x - destTile.x);
        int y_change = Mathf.Abs(sourceTile.y - destTile.y);

        int total_change  = x_change + y_change;

        //determine closest tile to destination tile and move
        while((total_change > unit.unit.speed || destTile.OccupiedUnit != null || destTile.OccupiedStructure != null) && destTile != sourceTile)
        {
            //check x and move one closer
            if(destTile.x > sourceTile.x){
                destTile = GetTileAtPosition(new Vector2(destTile.x-1,destTile.y));
            }else if(destTile.x < sourceTile.x){
                destTile = GetTileAtPosition(new Vector2(destTile.x+1,destTile.y));
            }
            

            x_change = Mathf.Abs(sourceTile.x - destTile.x);
            y_change = Mathf.Abs(sourceTile.y - destTile.y);

            total_change  = x_change + y_change;

            if((total_change > unit.unit.speed|| destTile.OccupiedUnit != null || destTile.OccupiedStructure != null) && destTile != sourceTile){
                //check y and move one closer
                if(destTile.y > sourceTile.y){
                    destTile = GetTileAtPosition(new Vector2(destTile.x,destTile.y-1));
                }else if(destTile.y < sourceTile.y){
                    destTile = GetTileAtPosition(new Vector2(destTile.x,destTile.y+1));
                }

                
                x_change = Mathf.Abs(sourceTile.x - destTile.x);
                y_change = Mathf.Abs(sourceTile.y - destTile.y);

                total_change  = x_change + y_change;
           
            }


        }


        if(unit.unit.Faction == Faction.Hero){
            Unit_Manager.instance.SetSelectedHero((BaseHero)null);
        }
        
        
        destTile.setUnit(unit);
        

        //remove movment token
        unit.isAbleToMove = false;

        //Activate movment Effect
        if(unit.unit.afterMoving != null){
            unit.unit.afterMoving.ActivateEffect(unit);
        }
        
    }

    public List<BaseHero> getHerosInCircleArea(Vector2 pos, int radius)
    {
        List<BaseHero> r = new List<BaseHero>();

        foreach (BaseHero h in _heroes)
        {
            //check if in x bounds 
            if(h.OccupiedTile.x <= pos.x+radius && h.OccupiedTile.x >= pos.x - radius)
            {
                //check if in y bounds
                if(h.OccupiedTile.y <= pos.y + radius && h.OccupiedTile.y >= pos.y - radius)
                {
                    r.Add(h);
                }
            }
        }

        return r;
    }

    public List<BaseHero> getHerosInInteractableArea(Vector2 pos, int speed){
        List<BaseHero> r = new List<BaseHero>();

        foreach (BaseHero h in _heroes)
        {

            int range = speed +2;
            float x_change = Mathf.Abs(pos.x - h.OccupiedTile.x);
            float y_change = Mathf.Abs(pos.y - h.OccupiedTile.y);
            float distance = x_change + y_change;

            if(x_change == range || y_change == range){
                //do nothing 
            }else if(distance <= range){
                r.Add(h);
            }

        }

        return r;
    }

    public BaseHero getClosestHero(Vector2 pos){
        BaseHero r = null;
        float LowestDistance = float.MaxValue;

        foreach (BaseHero h in _heroes)
        {
            
            float x_change = Mathf.Abs(pos.x - h.OccupiedTile.x);
            float y_change = Mathf.Abs(pos.y - h.OccupiedTile.y);
            float distance = x_change + y_change;

            if(distance < LowestDistance){
                r = h;
                LowestDistance = distance;
            }
        }
        return r;
    }

    public Tile FindNearestEmptyTile(Tile src, Tile dst){
        //get all adjacent Tiles of dst that are empty
        List<Tile> posibilities = new List<Tile>();

        for(int i = -1; i < 2; i++){
            for(int j = -1; j < 2; j++){
                if(GetTileAtPosition(new Vector2(dst.x + i, dst.y + j))==null){ }
                else if (GetTileAtPosition(new Vector2(dst.x + i, dst.y + j)).OccupiedUnit == null){
                    posibilities.Add(GetTileAtPosition(new Vector2(dst.x + i, dst.y + j)));
                }
            }
        }
        
        Tile result = src;
        float smallest_distance = float.MaxValue;

        foreach(Tile t in posibilities){
            float x_change = Mathf.Abs(t.x - src.x);
            float y_change = Mathf.Abs(t.y - src.y);

            float total_change  = x_change + y_change;

            if(total_change < smallest_distance){
                result = t;
                smallest_distance = total_change;
            }
        }

        return result;
    }

    //remove hero from list if it exists
    public bool RemoveHero(BaseHero h){

        if(_heroes.Contains(h)){
            _heroes.Remove(h);
            return true;
        }
        return false;
    }

    //remove hero from list if it exists
    public bool RemoveEnemy(BaseEnemy e){

        if(_enemies.Contains(e)){
            
            _enemies.Remove(e);

            Game_Manager.instance.CheckEnemyClearCondition();

            return true;
        }
        return false;
    }

    public bool removeEnemyStructure(EnemyStructure e){
        if(_EnemyStructures.Contains(e)){
            
            _EnemyStructures.Remove(e);

            Game_Manager.instance.CheckEnemyClearCondition();

            return true;
        }
        return false;
    }

    public bool removeNeutralStructure(NeutralStructure n){
        if(_NeutralStructures.Contains(n)){
            _NeutralStructures.Remove(n);

            if(_NeutralStructures.Count <= 0 ){
                Game_Manager.instance.ResolveNoNeutralStructures();
            }

            return true;
        }

        return false;
    }

    public bool removeAllyStructure(AllyStructure allyStructure){
        if(_AllyStructures.Contains(allyStructure)){
            _AllyStructures.Remove(allyStructure);
            return true;
        }
        return false;
    }

    public bool WithinOne(Tile s, Tile t){

        float x_change = Mathf.Abs(s.x - t.x);
        float y_change = Mathf.Abs(s.y - t.y);

        if( x_change <= 1 && y_change <= 1){
            return true;
        }
        else{
            return false;

        }
    }

    public void ActivateEnemyStructureEndOfTurnEffects(){
        foreach(Structure s in _EnemyStructures){
            s.ActivateEndOfTurnEffects();
        }

        foreach(Structure s in _NeutralStructures){
            s.ActivateEndOfTurnEffects();
        }

        foreach(BaseEnemy be in _enemies){
            be.ActivateEndOfTurnEffects();
        }
    }

    public void ActivateAllyStructureEndOfTurnEffects(){
        foreach(Structure s in _AllyStructures){
            s.ActivateEndOfTurnEffects();
        }

        foreach(Structure s in _NeutralStructures){
            s.ActivateEndOfTurnEffects();
        }

        foreach(BaseHero bh in _heroes){
            bh.ActivateEndOfTurnEffects();
        }
    }

    public Tile GetRandAdjactentFreeTile(Tile src){
        
        List<Tile> tiles = new List<Tile>();

        //load options for tile
        for(int x = -1;x <= 1;x++){
            for(int y = -1; y <= 1; y++){
                _tiles.TryGetValue(new Vector2(src.x + x, src.y+y),out Tile temp);

                if(temp != null){
                    if(temp.Walkable){
                        tiles.Add(temp);
                    }
                }
            }
        }

        if(tiles.Count == 0){
            return null;
        }

        //chose a random tile and return it
        int rand = Random.Range(0,tiles.Count-1);

        return tiles[rand];

    }

    public void ShowUnitActionTiles(BaseUnit baseUnit){

        int width = 0;
        int farthestTileDistance = baseUnit.unit.speed+2;
        
        //only show the attack indicator if the unit is unable to move
        if(!baseUnit.isAbleToMove){
            ShowAttackIndicatorOnly(baseUnit);
            return;
        }

        //traverse each surrounding tile based on unit speed and attack range (2 by defualt)
        for(int j = farthestTileDistance; j >= -farthestTileDistance;j--){

            for(int i = width; i >= -width; i--){
                Vector2 offset = new Vector2(i,j);
                Tile cTile = GetTileAtPosition(baseUnit.OccupiedTile,offset);

                if(cTile == null){
                    continue;
                }
                
                //set movement if no one is occupying otherwise set attack indicator 
                if(cTile.OccupiedStructure == null && cTile.OccupiedUnit == null){
                    cTile.SetMoveIndicator();
                }else{
                    if(cTile.OccupiedStructure != null){
                        if(cTile.OccupiedStructure._structure.Faction == Faction.Enemy){
                            if(baseUnit.isAbleToAttack){cTile.SetAttackIndicator();}
                        }
                    }
                    else if(cTile.OccupiedUnit != null){
                        if(cTile.OccupiedUnit.unit.Faction == Faction.Enemy){
                            if(baseUnit.isAbleToAttack){cTile.SetAttackIndicator();}  
                        }
                    }
                }

                int dist = GetDistanceBetweenTiles(baseUnit.OccupiedTile,cTile);

                //if the attack is out of movement range set to attack indicator
                if( dist > baseUnit.unit.speed){
                    if(baseUnit.isAbleToAttack){cTile.SetAttackIndicator();}
                    else{cTile.ClearTile();}
                }

                //determine if one of 4 corners and clear that tile if it is
                if((Mathf.Abs(i) == farthestTileDistance && j == 0) || (Mathf.Abs(j) == farthestTileDistance && i == 0)){
                    cTile.ClearTile();
                }


            }
            
            if(j>0){
                width++;
            }else{
                width--;
            }
            
        }
    }

    //goes through each hero and shows there summonable area tiles
    public void ShowSummonableTiles(Card card){

        foreach(BaseHero hero in _heroes){
            //Debug.Log("Checking" + hero.name);
            ShowSummonIndicator(hero);
        }
    }

    public void ShowAttackIndicatorOnly(BaseUnit baseUnit){
        if(!baseUnit.isAbleToAttack){
            return;
        }

        for(int i = 1; i >= -1; i--){
            for(int j = 1; j >= -1; j--){

                Vector2 offset = new Vector2(i,j);
                Tile cTile = GetTileAtPosition(baseUnit.OccupiedTile,offset);

                if(cTile != null){
                    if(cTile.OccupiedUnit != null){
                        if(cTile.OccupiedUnit.unit.Faction == Faction.Enemy || cTile.OccupiedUnit.unit.Faction == Faction.Neutral){
                            cTile.SetAttackIndicator();
                        }
                    }else if(cTile.OccupiedStructure != null){
                        if(cTile.OccupiedStructure._structure.Faction == Faction.Enemy || cTile.OccupiedStructure._structure.Faction == Faction.Neutral){
                            cTile.SetAttackIndicator();
                        }
                    }
                }

            }
        }
    }

    // shows the summonable locations surrounding this hero unit
    public void ShowSummonIndicator(BaseHero hero){
        for(int i = 1; i >= -1; i--){
            for(int j = 1; j >= -1; j--){

                Vector2 offset = new Vector2(i,j);
                Tile cTile = GetTileAtPosition(hero.OccupiedTile,offset);

                if(cTile != null){
                    if(cTile.OccupiedUnit == null && cTile.OccupiedStructure == null){
                        cTile.SetMoveIndicator();   
                    }
                }

            }
        }
    }

    //clears all the indicators on the board
    public void ClearBoardIndicators(){
        foreach(Vector2 tLocation in _tiles.Keys){
            Tile tile = GetTileAtPosition(tLocation);
            tile.ClearTile();
        }
    }

    public void ApplyFieldBuffs(){
        foreach(BaseHero bh in _heroes){
            bh.currentAttack += allyAttackBuff;
            bh.UpdateAttackAndHealthDisplay();
        }

        foreach(BaseEnemy be in _enemies){
            be.currentAttack += enemyAttackBuff;
            be.UpdateAttackAndHealthDisplay();
        }
    }

    public void ClearFieldBuffs(){
        foreach(BaseHero bh in _heroes){
            bh.currentAttack = bh.unit.attack;
            bh.UpdateAttackAndHealthDisplay();
        }

        foreach(BaseEnemy be in _enemies){
            be.currentAttack = be.unit.attack;
            be.UpdateAttackAndHealthDisplay();
        }
    }

    public Card GetRewardCard(){
        return _map.reward;
    }

    public void LoadData(PlayerData playerData)
    {

        _map = DataPersistanceManager.instance.mapIDTable.getMap(playerData.CombatMap);
        Debug.Log("Map Set: " + _map.name);
    }

    public void SaveData(ref PlayerData playerData)
    {
        Debug.Log("DeckBuilder Not Currently saving Data");
    }

    

}
