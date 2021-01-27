// ----------------------------------------------------------------------------
// The MIT License
// Ui extension https://github.com/Leopotam/ecs-ui
// for ECS framework https://github.com/Leopotam/ecs
// Copyright (c) 2017-2020 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Leopotam.Ecs.Ui.Systems {
    /// <summary>
    /// Emitter system for uGui events to ECS world.
    /// </summary>
    public class EcsUiEmitter : MonoBehaviour {
        internal EcsWorld World = null;
        readonly Dictionary<int, GameObject> _actions = new Dictionary<int, GameObject> (64);

        /// <summary>
        /// Gets attached after InjectUi() call world instance.
        /// </summary>
        public EcsWorld GetWorld () {
            return World;
        }

        /// <summary>
        /// Creates ecs entity for message.
        /// </summary>
        public EcsEntity CreateEntity () {
            ValidateEcsFields ();
            return World.NewEntity ();
        }

        /// <summary>
        /// Sets link to named GameObject to use it later from code. If GameObject is null - unset named link.
        /// </summary>
        /// <param name="widgetName">Logical name.</param>
        /// <param name="go">GameObject link.</param>
        public void SetNamedObject (string widgetName, GameObject go) {
            if (!string.IsNullOrEmpty (widgetName)) {
                var id = widgetName.GetHashCode ();
                if (_actions.ContainsKey (id)) {
                    if (!go) {
                        _actions.Remove (id);
                    } else {
                        throw new Exception ($"Action with \"{widgetName}\" name already registered");
                    }
                } else {
                    if ((object) go != null) {
                        _actions[id] = go.gameObject;
                    }
                }
            }
        }

        /// <summary>
        /// Gets link to named GameObject to use it later from code.
        /// </summary>
        /// <param name="widgetName">Logical name.</param>
        public GameObject GetNamedObject (string widgetName) {
            _actions.TryGetValue (widgetName.GetHashCode (), out var retVal);
            return retVal;
        }

        [System.Diagnostics.Conditional ("DEBUG")]
        void ValidateEcsFields () {
#if DEBUG
            if (World == null) {
                throw new Exception ("[EcsUiEmitter] Call EcsSystems.InjectUi() first.");
            }
#endif
        }
    }
}