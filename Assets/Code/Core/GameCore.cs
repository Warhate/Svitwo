using UnityEngine;
using System.Collections;
using Svitwo;

namespace Svitwo
{
    public class GameCore : MonoBehaviour
    {
        #region DEV

        public const bool IS_DEV = true;

        #endregion

        bool _inited;

        EntityManager   _entityManager;
        Logger          _logger;

        void Start()
        {
            Init();
        }

        bool Init()
        {
            if (_inited)
            {
                return false;
            }

            _logger = new Logger(new DevLogHandler()) {logEnabled = IS_DEV};

            _entityManager = new EntityManager();

            _inited = true;

            return true;
        }

        void Update()
        {

        }
    }
}
