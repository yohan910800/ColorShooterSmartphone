using UnityEngine;
using MankindGames;

public class RedRouteBossPattern1Weapon2: Weapon {

    // / <summary> / **for RedRoute Boss Phase1,Pattern1.** / discription- /
    // 1.shoots multiple bullets at the same time(360' burst). (Done) /
    // 2. Player can't move between bullets.(Adjustable) /              3. /
    // 4. / / / </summary>

    int bulletAmount = 50;

    float startAngle = 0f;
    float endAngle = 360f;

    public override void Init(ICharacter character, Transform[] sockets) {
        damage = 10;
        fireRate = 2;
        bulletSpeed = 10;
        barrelLen = 2f;
        spriteName = "SingleShooter";
        bulletName = "StraightShotM";
        base.Init(character, sockets);

    }

    public override bool Shoot(GameObject target, Vector3 aimDir) {
        foreach(Transform t in sockets) {

            float angleStep = (endAngle - startAngle) / bulletAmount;
            float angle = startAngle;
            GameObject Target = GameObject.FindGameObjectWithTag("Player");
            for (int i = 0; i < bulletAmount + 1; i++) {
                float directionX = t.position.x + Mathf.Sin(Random.Range(0, 359));
                float directionY = t.position.y + Mathf.Cos(Random.Range(0, 359));

                Vector3 bulletDirection = new Vector3(directionX, directionY, 0f);
                Vector3 direction = bulletDirection - t.position;
                direction = direction.normalized;

                GameObject bulletObj = MonoBehaviour.Instantiate(
                    bulletPrefab,
                    t.position + (direction * barrelLen),
                    Quaternion.identity
                )as GameObject;

                ChangeColor(Colors.Red);
                Bullet bullet = bulletObj.GetComponent<Bullet>();
                bullet.Init(owner, direction, bulletSpeed, damage, range, bulletColor);

                angle += angleStep;
            }

        }
        return true;
    }

}
