using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Nebby.Serialization
{
    public static class StringSerializer
    {
        private delegate string SerializationDelegate(object obj);
        private delegate object DeserializationDelegate(string obj);
        private struct SerializationHandler
        {
            public SerializationDelegate serialize;
            public DeserializationDelegate deserialize;
        }

        [Serializable]
        private struct AnimationCurveJsonIntermediate
        {
            public WrapMode preWrapMode;

            public WrapMode postWrapMode;

            public KeyframeJsonIntermediate[] keys;

            public static AnimationCurve ToAnimationCurve(in AnimationCurveJsonIntermediate intermediate)
            {
                KeyframeJsonIntermediate[] array = intermediate.keys ?? Array.Empty<KeyframeJsonIntermediate>();
                Keyframe[] array2 = new Keyframe[array.Length];
                for (int i = 0; i < array.Length; i++)
                {
                    array2[i] = KeyframeJsonIntermediate.ToKeyframe(in array[i]);
                }
                return new AnimationCurve
                {
                    preWrapMode = intermediate.preWrapMode,
                    postWrapMode = intermediate.postWrapMode,
                    keys = array2
                };
            }

            public static AnimationCurveJsonIntermediate FromAnimationCurve(AnimationCurve src)
            {
                Keyframe[] array = src.keys;
                KeyframeJsonIntermediate[] array2 = new KeyframeJsonIntermediate[array.Length];
                for (int i = 0; i < array.Length; i++)
                {
                    array2[i] = KeyframeJsonIntermediate.FromKeyframe(array[i]);
                }
                AnimationCurveJsonIntermediate result = default(AnimationCurveJsonIntermediate);
                result.preWrapMode = src.preWrapMode;
                result.postWrapMode = src.postWrapMode;
                result.keys = array2;
                return result;
            }
        }

        [Serializable]
        private struct KeyframeJsonIntermediate
        {
            public float time;

            public float value;

            public float inTangent;

            public float outTangent;

            public float inWeight;

            public float outWeight;

            public WeightedMode weightedMode;

            public int tangentMode;

            public static Keyframe ToKeyframe(in KeyframeJsonIntermediate intermediate)
            {
                Keyframe result = default(Keyframe);
                result.time = intermediate.time;
                result.value = intermediate.value;
                result.inTangent = intermediate.inTangent;
                result.outTangent = intermediate.outTangent;
                result.inWeight = intermediate.inWeight;
                result.outWeight = intermediate.outWeight;
                result.weightedMode = intermediate.weightedMode;
                result.tangentMode = intermediate.tangentMode;
                return result;
            }

            public static KeyframeJsonIntermediate FromKeyframe(Keyframe src)
            {
                KeyframeJsonIntermediate result = default(KeyframeJsonIntermediate);
                result.time = src.time;
                result.value = src.value;
                result.inTangent = src.inTangent;
                result.outTangent = src.outTangent;
                result.inWeight = src.inWeight;
                result.outWeight = src.outWeight;
                result.weightedMode = src.weightedMode;
                result.tangentMode = src.tangentMode;
                return result;
            }
        }

        private static readonly Dictionary<Type, SerializationHandler> typeToHandler = new Dictionary<Type, SerializationHandler>();
        private static CultureInfo Invariant => CultureInfo.InvariantCulture;

        private static string[] SplitToComponents(string value, Type type, int minComponentCount)
        {
            string[] array = value.Split(' ');
            if(array.Length < minComponentCount)
            {
                throw new FormatException($"Too few elements ({array.Length}/{minComponentCount}) for {type.FullName}");
            }
            return array;
        }

        public static bool CanSerializeType<T>()
        {
            return typeToHandler.ContainsKey(typeof(T));
        }

        public static bool CanSerializeType(Type type)
        {
            return typeToHandler.ContainsKey(type);
        }

        public static string Serialize(Type type, object value)
        {
            if(typeToHandler.TryGetValue(type, out var handler))
            {
                try
                {
                    return handler.serialize(value);
                }
                catch(Exception e)
                {
                    throw new FormatException($"Error on string serializer", e);
                }
            }
            return string.Empty;
        }

        public static object Deserialize(Type type, string value)
        {
            if(typeToHandler.TryGetValue(type, out var handler))
            {
                return handler.deserialize(value);
            }
            return null;
        }
        static StringSerializer()
        {
            typeToHandler.Add(typeof(int), new SerializationHandler
            {
                serialize = (obj) => ((int)obj).ToString(Invariant),
                deserialize = (str) => int.Parse(str, Invariant)
            });

            typeToHandler.Add(typeof(bool), new SerializationHandler
            {
                serialize = (obj) => ((bool)obj) ? "1" : "0",
                deserialize = (str) => int.Parse(str, Invariant) > 0
            });

            typeToHandler.Add(typeof(float), new SerializationHandler
            {
                serialize = (obj) => ((float)obj).ToString(Invariant),
                deserialize = (str) => float.Parse(str, Invariant)
            });

            typeToHandler.Add(typeof(string), new SerializationHandler
            {
                serialize = (obj) => (string)obj,
                deserialize = (str) => str
            });

            typeToHandler.Add(typeof(Color), new SerializationHandler
            {
                serialize = (obj) =>
                {
                    Color color = (Color)obj;
                    return $"{color.r.ToString(Invariant)} {color.g.ToString(Invariant)} {color.b.ToString(Invariant)} {color.a.ToString(Invariant)}";
                },
                deserialize = (str) =>
                {
                    string[] components = SplitToComponents(str, typeof(Color), 4);
                    return new Color(float.Parse(components[0], Invariant), float.Parse(components[1], Invariant), float.Parse(components[2], Invariant), float.Parse(components[3], Invariant));
                }
            });

            typeToHandler.Add(typeof(LayerMask), new SerializationHandler
            {
                serialize = (obj) =>
                {
                    LayerMask mask = (LayerMask)obj;
                    return mask.value.ToString(Invariant);
                },
                deserialize = (str) =>
                {
                    LayerMask mask = new LayerMask { value = int.Parse(str, Invariant) };
                    return mask;
                }
            });

            typeToHandler.Add(typeof(Vector2), new SerializationHandler
            {
                serialize = (obj) =>
                {
                    Vector2 vector = (Vector2)obj;
                    return $"{vector.x.ToString(Invariant)} {vector.y.ToString(Invariant)}";
                },
                deserialize = (str) =>
                {
                    string[] components = SplitToComponents(str, typeof(Vector2), 2);
                    return new Vector2(float.Parse(components[0], Invariant), float.Parse(components[1], Invariant));
                }
            });

            typeToHandler.Add(typeof(Vector3), new SerializationHandler
            {
                serialize = (obj) =>
                {
                    Vector3 vector = (Vector3)obj;
                    return $"{vector.x.ToString(Invariant)} {vector.y.ToString(Invariant)} {vector.z.ToString(Invariant)}";
                },
                deserialize = (str) =>
                {
                    string[] components = SplitToComponents(str, typeof(Vector3), 3);
                    return new Vector3(float.Parse(components[0], Invariant), float.Parse(components[1], Invariant), float.Parse(components[2], Invariant));
                }
            });

            typeToHandler.Add(typeof(Vector4), new SerializationHandler
            {
                serialize = (obj) =>
                {
                    Vector4 vector = (Vector4)obj;
                    return $"{vector.x.ToString(Invariant)} {vector.y.ToString(Invariant)} {vector.z.ToString(Invariant)} {vector.w.ToString(Invariant)}";
                },
                deserialize = (str) =>
                {
                    string[] components = SplitToComponents(str, typeof(Vector4), 4);
                    return new Vector4(float.Parse(components[0], Invariant), float.Parse(components[1], Invariant), float.Parse(components[2], Invariant), float.Parse(components[3], Invariant));
                }
            });

            typeToHandler.Add(typeof(Rect), new SerializationHandler
            {
                serialize = (obj) =>
                {
                    Rect rect = (Rect)obj;
                    return $"{rect.x.ToString(Invariant)} {rect.y.ToString(Invariant)} {rect.width.ToString(Invariant)} {rect.height.ToString(Invariant)}";
                },
                deserialize = (str) =>
                {
                    string[] components = SplitToComponents(str, typeof(Rect), 4);
                    return new Rect(float.Parse(components[0], Invariant), float.Parse(components[1], Invariant), float.Parse(components[2], Invariant), float.Parse(components[3], Invariant));
                }
            });

            typeToHandler.Add(typeof(char), new SerializationHandler
            {
                serialize = (obj) =>
                {
                    char character = (char)obj;
                    return character.ToString(Invariant);
                },
                deserialize = (str) =>
                {
                    return char.Parse(str);
                }
            });

            typeToHandler.Add(typeof(AnimationCurve), new SerializationHandler
            {
                serialize = (obj) => JsonUtility.ToJson(AnimationCurveJsonIntermediate.FromAnimationCurve((AnimationCurve)obj)),
                deserialize = (str) =>
                {
                    if (string.IsNullOrEmpty(str))
                        return new AnimationCurve();
                    AnimationCurveJsonIntermediate intermediate = JsonUtility.FromJson<AnimationCurveJsonIntermediate>(str);
                    return AnimationCurveJsonIntermediate.ToAnimationCurve(in intermediate);
                }
            });

            typeToHandler.Add(typeof(Bounds), new SerializationHandler
            {
                serialize = (obj) =>
                {
                    Bounds bounds = (Bounds)obj;
                    return $"{bounds.center.x.ToString(Invariant)} {bounds.center.y.ToString(Invariant)} {bounds.center.z.ToString(Invariant)} " +
                    $"{bounds.size.x.ToString(Invariant)} {bounds.size.y.ToString(Invariant)} {bounds.size.z.ToString(Invariant)}";
                },
                deserialize = (str) =>
                {
                    string[] components = SplitToComponents(str, typeof(Bounds), 6);
                    Vector3 center = new Vector3(float.Parse(components[0], Invariant), float.Parse(components[1], Invariant), float.Parse(components[2], Invariant));
                    Vector3 size = new Vector3(float.Parse(components[3], Invariant), float.Parse(components[4], Invariant), float.Parse(components[5], Invariant));
                    return new Bounds(center, size);
                }
            });

            typeToHandler.Add(typeof(Quaternion), new SerializationHandler
            {
                serialize = (obj) =>
                {
                    Quaternion quat = (Quaternion)obj;
                    return $"{quat.x.ToString(Invariant)} {quat.y.ToString(Invariant)} {quat.z.ToString(Invariant)} {quat.w.ToString(Invariant)}";
                },
                deserialize = (str) =>
                {
                    string[] components = SplitToComponents(str, typeof(Quaternion), 4);
                    return new Quaternion(float.Parse(components[0], Invariant), float.Parse(components[1], Invariant), float.Parse(components[2], Invariant), float.Parse(components[3], Invariant));
                }
            });

            typeToHandler.Add(typeof(BoundsInt), new SerializationHandler
            {
                serialize = (obj) =>
                {
                    BoundsInt bounds = (BoundsInt)obj;
                    return $"{bounds.center.x.ToString(Invariant)} {bounds.center.y.ToString(Invariant)} {bounds.center.z.ToString(Invariant)} " +
                    $"{bounds.size.x.ToString(Invariant)} {bounds.size.y.ToString(Invariant)} {bounds.size.z.ToString(Invariant)}";
                },
                deserialize = (str) =>
                {
                    string[] components = SplitToComponents(str, typeof(BoundsInt), 6);
                    Vector3Int center = new Vector3Int(int.Parse(components[0], Invariant), int.Parse(components[1], Invariant), int.Parse(components[2], Invariant));
                    Vector3Int size = new Vector3Int(int.Parse(components[3], Invariant), int.Parse(components[4], Invariant), int.Parse(components[5], Invariant));
                    return new Bounds(center, size);
                }
            });

            typeToHandler.Add(typeof(RectInt), new SerializationHandler
            {
                serialize = (obj) =>
                {
                    RectInt rect = (RectInt)obj;
                    return $"{rect.x.ToString(Invariant)} {rect.y.ToString(Invariant)} {rect.width.ToString(Invariant)} {rect.height.ToString(Invariant)}";
                },
                deserialize = (str) =>
                {
                    string[] components = SplitToComponents(str, typeof(Rect), 4);
                    return new RectInt(int.Parse(components[0], Invariant), int.Parse(components[1], Invariant), int.Parse(components[2], Invariant), int.Parse(components[3], Invariant));
                }
            });

            typeToHandler.Add(typeof(Vector2Int), new SerializationHandler
            {
                serialize = (obj) =>
                {
                    Vector2Int vector = (Vector2Int)obj;
                    return $"{vector.x.ToString(Invariant)} {vector.y.ToString(Invariant)}";
                },
                deserialize = (str) =>
                {
                    string[] components = SplitToComponents(str, typeof(Vector2Int), 2);
                    return new Vector2Int(int.Parse(components[0], Invariant), int.Parse(components[1], Invariant));
                }
            });

            typeToHandler.Add(typeof(Vector3Int), new SerializationHandler
            {
                serialize = (obj) =>
                {
                    Vector3Int vector = (Vector3Int)obj;
                    return $"{vector.x.ToString(Invariant)} {vector.y.ToString(Invariant)} {vector.z.ToString(Invariant)}";
                },
                deserialize = (str) =>
                {
                    string[] components = SplitToComponents(str, typeof(Vector3Int), 3);
                    return new Vector3Int(int.Parse(components[0], Invariant), int.Parse(components[1], Invariant), int.Parse(components[2], Invariant));
                }
            });
        }
    }
}