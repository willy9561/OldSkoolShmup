using org.flixel;

namespace OldSkoolShmup
{
    public class PlayState : FlxState
    {
        private const uint maxBufSize = 256;
        private const int maxLevelLimit = 20;
        private byte[] EnemyFireBuf = new byte[maxBufSize];
        private byte[] EnemyMoveBuf = new byte[maxBufSize];
        private byte[] EnemyUpdateBuf = new byte[maxBufSize];
	    private byte[] EnemyStratBuf = new byte[maxBufSize];
        private FlxArray<Bullet> _bullets;
        private FlxArray<Enemy> _Enemies;
        private FlxArray<Bullet> _enemyBullets;
        private Player _player;
        private double _restart;
        private bool m_newAttacker;
        private bool m_cleared;
        private int AttackMode;
        private int m_Stage;
        private int m_Pattern;
        private int k;
        private double count;

        private byte enemyMoveBufPut;
        private byte enemyMoveBufGet;
        private byte enemyMoveBufCnt;
        private byte indexMoveA;
        private Enemy _mEnemyMoveA;

        private byte enemyUpdateBufPut;
        private byte enemyUpdateBufGet;
        private byte enemyUpdateBufCnt;
        private byte indexUpdateA;
        private Enemy _mEnemyUpdateA;

        private byte enemyFireBufPut;
        private byte enemyFireBufGet;
        private byte enemyFireBufCnt;
        private byte indexFireA;
        private Enemy _mEnemyFireA;


        public PlayState()
        {
            _restart = 0;
            k=0;
            m_newAttacker = false;
            m_cleared = false;
            m_Stage = FlxG.level;
            m_Pattern = 2;

            //Move Buffer
            enemyMoveBufPut = 0;
            enemyMoveBufGet = 0;
            enemyMoveBufCnt = 0;
            _mEnemyMoveA = null;
            indexMoveA = 0;

            //Fire Buffer
            enemyFireBufPut = 0;
            enemyFireBufGet = 0;
            enemyFireBufCnt = 0;
            _mEnemyFireA = null;
            indexFireA = 0;

            //Update Buffer
            enemyUpdateBufPut = 0;
            enemyUpdateBufGet = 0;
            enemyUpdateBufCnt = 0;
            _mEnemyUpdateA = null;
            indexUpdateA = 0;


            _bullets = new FlxArray<Bullet>();
            for (int i = 0; i < 8; i++)
                _bullets.add(this.add(new Bullet()) as Bullet);

            _player = new Player(((FlxG.width/2) - 10), (FlxG.height - 20), _bullets);
            this.add(_player);

            _Enemies = new FlxArray<Enemy>();

            int levelLimit = 10 * (FlxG.level +1);

            if (levelLimit > maxLevelLimit)
                levelLimit = maxLevelLimit;

            int HorizontalScreenWidth = FlxG.width;
            int TopRowOffset = 20;
            int EnemySpriteWidth = 32;

            for (int i = 0; i < levelLimit; i++)
            {
                if (i > 5)
                    m_Pattern = 1;

                _enemyBullets = new FlxArray<Bullet>();
                for (int j = 0; j < 8; j++)
                    _enemyBullets.add(this.add(new Bullet()) as Bullet);

                if (i < 10)
                    _Enemies.add(this.add(new Alien1((HorizontalScreenWidth - EnemySpriteWidth * (i + 1)), TopRowOffset, i, m_Stage, m_Pattern, _enemyBullets)) as Enemy);
                if (i >= 10)
                    _Enemies.add(this.add(new Alien1((HorizontalScreenWidth - EnemySpriteWidth * ((i + 1) - 10)), (TopRowOffset * 2), i, m_Stage, m_Pattern, _enemyBullets)) as Enemy);

                _Enemies[i].setUpdateRequest(this);
            }

            

        }
        public override void update()
        {
            FlxG.overlapArrays(_bullets, _Enemies, bulletHitAlien);
            FlxG.overlapArray(_Enemies, _player, AlienHitPlayer);


            for (int i = 0; i < _Enemies.length; i++)
            {
                FlxG.overlapArray(_Enemies[i].Bullets, _player, bulletHitAlien);
            }

            //enemy move requests
            if (enemyMoveBufCnt > 0)
            {
                indexMoveA = getBufferMoveEnemy();
                _mEnemyMoveA = _Enemies[indexMoveA];
                _mEnemyMoveA.move();
            }
            else
            {
                _mEnemyMoveA = null;
            }

            if (enemyUpdateBufCnt > 0)
            {
                indexUpdateA = getBufferUpdateEnemy();
                _mEnemyUpdateA = _Enemies[indexUpdateA];
                _mEnemyUpdateA.update(); 
            }
            else
            {
                _mEnemyUpdateA = null;
            }

            if (enemyFireBufCnt > 0)
            {
                indexFireA = getBufferFireEnemy();
                _mEnemyFireA = _Enemies[indexFireA];
                _mEnemyFireA.fire();
            }
            else
            {
                _mEnemyFireA = null;
            }

            if ((_Enemies[k].dead == true) && (m_newAttacker == false))
            {
                if (_Enemies[k].uid == AttackMode)
                {
                    Enemy temp = FlxArray<Enemy>.getRandom(_Enemies);
                    AttackMode = temp.uid;
                    m_newAttacker = true;
                }

            }
            else if (AttackMode == 0)
            {

                Enemy temp = FlxArray<Enemy>.getRandom(_Enemies);
                AttackMode = temp.uid;
                         
            }
            else if (m_newAttacker == true)
            {
                count += FlxG.elapsed;
                if (count > 1.0)
                {
                    m_newAttacker = false;
                    count = 0;
                }
            }

            _Enemies[k].m_AttackMode = AttackMode;
 
            
            k++;
            if (k >= _Enemies.length)
              k = 0;

            //game restart timer
            if (_player.dead)
            {
                if (_player.m_explode.finished)
                {
                    FlxG.level = 0;
                    _restart += FlxG.elapsed;
                    if (_restart > 2)
                        FlxG.switchState(typeof(Credits));
                    return;
                }
            }
            for (int i = 0; i < _Enemies.length; i++)
            {
                m_cleared = true;

                if (_Enemies[i].exists == true)
                {
                    m_cleared = false;
                    break;
                }
            }
            if (m_cleared == true)
            {
                FlxG.level++;
                FlxG.switchState(typeof(PlayState));
            }

            base.update();
        }
        public void OnFade()
        {
            FlxG.switchState(typeof(Credits));
        }

        private void bulletHitAlien(FlxCore Bullet, FlxCore Enemy)
        {
            (Bullet as FlxSprite).hurt(0);
            (Enemy as FlxSprite).hurt(1);
        }

        private void AlienHitPlayer(FlxCore Enemy, FlxCore Player)
        {
            (Enemy as FlxSprite).hurt(1);
            (Player as FlxSprite).hurt(1);
        }

        public void enemyMove(byte enemyId)
        {
	        putBufferMoveEnemy(enemyId);
        }
        private void putBufferMoveEnemy(byte enemyId)
        {
	        enemyMoveBufCnt++;
	        EnemyMoveBuf[enemyMoveBufPut++] = enemyId;
	        
        }
        private byte getBufferMoveEnemy()
        {
	        enemyMoveBufCnt--;
	        byte mTemp = EnemyMoveBuf[enemyMoveBufGet++];
	        
	        return mTemp;

        }

        public void enemyUpdate(byte enemyId)
        {
	        putBufferUpdateEnemy(enemyId);
        }
        private void putBufferUpdateEnemy(byte enemyId)
        {
	        enemyUpdateBufCnt++;
	        EnemyUpdateBuf[enemyUpdateBufPut++] = enemyId;
	        

        }
        private byte getBufferUpdateEnemy()
        {
	        enemyUpdateBufCnt--;
	        byte mTemp = EnemyUpdateBuf[enemyUpdateBufGet++];
	        
	        return mTemp;
        }

        public void enemyFire(byte enemyId)
        {
            putBufferFireEnemy(enemyId);
        }
        private void putBufferFireEnemy(byte enemyId)
        {
            enemyFireBufCnt++;
            EnemyFireBuf[enemyFireBufPut++] = enemyId;


        }
        private byte getBufferFireEnemy()
        {
            enemyFireBufCnt--;
            byte mTemp = EnemyFireBuf[enemyFireBufGet++];

            return mTemp;
        }
        
    }

 }
