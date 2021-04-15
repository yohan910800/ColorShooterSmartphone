using UnityEngine;
using MankindGames;

public class RedRouteBossPattern2Weapon1 : Weapon
{

    // / <summary> / **for RedRoute Boss Phase1,Pattern1.** / discription- /
    // 1.shoots multiple bullets at the same time(360' burst). (Done) /
    // 2. Player can't move between bullets.(Adjustable) /              3. /
    // 4. / / / </summary>

    int bulletAmount = 30;
    float bulletSpread = 1;

    Vector3 spawnReferencePoint;
    Vector3 bulletSpawnPoint;

    public override void Init(ICharacter character, Transform[] sockets)
    {
        barrelLen = 0.1f;
        damage = 1;
        fireRate = 2f;
        bulletSpeed=10;
        range=10f;
        spriteName = "SingleShooter";
        bulletName = "StraightShotM";
        base.Init(character, sockets);

    }

    public override bool Shoot(GameObject target, Vector3 aimDir)
    {
        foreach (Transform t in sockets)
        {
            // ------------------------------------------
            // Determin Direction and spawnpoint from Pattern
            // ------------------------------------------
            float directionX = 0;
            float directionY = 0;
            spawnReferencePoint = t.position;
            int pattern=Random.Range(0,3);
            switch (pattern)
            {
                // pattern- 0=shoots down from North
                case 0:
                    directionY = -1;
                    spawnReferencePoint.y = t.position.y + 15f;
                    break; // 1="" left from East

                case 1:
                    directionX = -1;
                    spawnReferencePoint.x = t.position.x + 15f;
                    break; // 2="" up from South

                case 2:
                    directionY = 1;
                    spawnReferencePoint.y = t.position.y - 15f;
                    break;

                case 3: // 3="" right from West
                    directionX = +1;
                    spawnReferencePoint.x = t.position.x - 15f;
                    break;
                default:
                    Log.log("Pattern interval out of boundary must be an int between 0~3");
                    break;


            }

            Vector3 direction = (new Vector3(directionX, directionY, 0)).normalized;

            // ------------------------------------------
            //1. set a starting point and add bullet spread
            //2. instantiate bullets
            // ------------------------------------------

            for (int i = 0; i < bulletAmount + 1; i++)
            {
                float bulletStartPointOffset = (bulletSpread * bulletAmount) / 2;
                if (directionX == 0)
                {
                    Vector3 bulletStartPoint = new Vector3(
                        spawnReferencePoint.x - bulletStartPointOffset,
                        spawnReferencePoint.y,
                        0
                    );
                    bulletSpawnPoint = bulletStartPoint;
                    bulletSpawnPoint.x += bulletSpread * i;
                }
                else
                {
                    Vector3 bulletStartPoint = new Vector3(
                        spawnReferencePoint.x,
                        spawnReferencePoint.y - bulletStartPointOffset,
                        0
                    );
                    bulletSpawnPoint = bulletStartPoint;
                    bulletSpawnPoint.y += bulletSpread * i;
                }

                GameObject bulletObj = MonoBehaviour.Instantiate(
                    bulletPrefab,
                    bulletSpawnPoint,
                    Quaternion.identity
                ) as GameObject;

                ChangeColor(Colors.Red);
                Bullet bullet = bulletObj.GetComponent<Bullet>();
                bullet.Init(owner, direction,bulletSpeed, damage,range, bulletColor);

            }
            
        }
        return true;
    }

}
