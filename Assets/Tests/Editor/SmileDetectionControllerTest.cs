using Emojis;
using NUnit.Framework;
using UnityEngine;

namespace Tests.Editor
{
    public class SmileDetectionControllerTest
    {
        private SmileDetectionController smileDetectionController;

        [SetUp]
        public void Init()
        {
            SmileDetectionConfig config = ScriptableObject.CreateInstance<SmileDetectionConfig>();
            config.minVeryHappyValue = .66f;
            config.minHappyValue = .33f;

            smileDetectionController = new SmileDetectionController(config);
        }

        [Test]
        public void ReturnNeutralWhenEmptyJsonGiven()
        {
            const string json = "[]";

            var result = smileDetectionController.DetectSmile(json);

            Assert.AreEqual(SmileType.Neutral, result);
        }

        [Test]
        public void ReturnNeutralWhenNoFaceAttributesGiven()
        {
            const string json = "[{\"faceRectangle\":{\"top\":325,\"left\":104,\"width\":398,\"height\":398}}]";

            var result = smileDetectionController.DetectSmile(json);

            Assert.AreEqual(SmileType.Neutral, result);
        }

        [Test]
        public void ReturnNeutralWhenNoSmileGiven()
        {
            const string json =
                "[{\"faceRectangle\":{\"top\":325,\"left\":104,\"width\":398,\"height\":398},\"faceAttributes\":{\"headPose\":{\"pitch\":-0.1,\"roll\":-2.8,\"yaw\":-22.1},\"gender\":\"female\",\"age\":20.0,\"facialHair\":{\"moustache\":0.0,\"beard\":0.0,\"sideburns\":0.0},\"glasses\":\"ReadingGlasses\",\"emotion\":{\"anger\":0.0,\"contempt\":0.0,\"disgust\":0.0,\"fear\":0.0,\"happiness\":1.0,\"neutral\":0.0,\"sadness\":0.0,\"surprise\":0.0},\"makeup\":{\"eyeMakeup\":true,\"lipMakeup\":true},\"hair\":{\"bald\":0.13,\"invisible\":false,\"hairColor\":[{\"color\":\"brown\",\"confidence\":0.99},{\"color\":\"black\",\"confidence\":0.52},{\"color\":\"red\",\"confidence\":0.39},{\"color\":\"blond\",\"confidence\":0.37},{\"color\":\"gray\",\"confidence\":0.15},{\"color\":\"other\",\"confidence\":0.13}]}}}]";

            var result = smileDetectionController.DetectSmile(json);

            Assert.AreEqual(SmileType.Neutral, result);
        }

        [Test]
        public void ReturnNeutralWhenNeutralSmileGiven()
        {
            const string json =
                "[{\"faceRectangle\":{\"top\":325,\"left\":104,\"width\":398,\"height\":398},\"faceAttributes\":{\"smile\":0.11,\"headPose\":{\"pitch\":-0.1,\"roll\":-2.8,\"yaw\":-22.1},\"gender\":\"female\",\"age\":20.0,\"facialHair\":{\"moustache\":0.0,\"beard\":0.0,\"sideburns\":0.0},\"glasses\":\"ReadingGlasses\",\"emotion\":{\"anger\":0.0,\"contempt\":0.0,\"disgust\":0.0,\"fear\":0.0,\"happiness\":1.0,\"neutral\":0.0,\"sadness\":0.0,\"surprise\":0.0},\"makeup\":{\"eyeMakeup\":true,\"lipMakeup\":true},\"hair\":{\"bald\":0.13,\"invisible\":false,\"hairColor\":[{\"color\":\"brown\",\"confidence\":0.99},{\"color\":\"black\",\"confidence\":0.52},{\"color\":\"red\",\"confidence\":0.39},{\"color\":\"blond\",\"confidence\":0.37},{\"color\":\"gray\",\"confidence\":0.15},{\"color\":\"other\",\"confidence\":0.13}]}}}]";

            var result = smileDetectionController.DetectSmile(json);

            Assert.AreEqual(SmileType.Neutral, result);
        }

        [Test]
        public void ReturnHappyWhenHappySmileGiven()
        {
            const string json =
                "[{\"faceRectangle\":{\"top\":325,\"left\":104,\"width\":398,\"height\":398},\"faceAttributes\":{\"smile\":0.47,\"headPose\":{\"pitch\":-0.1,\"roll\":-2.8,\"yaw\":-22.1},\"gender\":\"female\",\"age\":20.0,\"facialHair\":{\"moustache\":0.0,\"beard\":0.0,\"sideburns\":0.0},\"glasses\":\"ReadingGlasses\",\"emotion\":{\"anger\":0.0,\"contempt\":0.0,\"disgust\":0.0,\"fear\":0.0,\"happiness\":1.0,\"neutral\":0.0,\"sadness\":0.0,\"surprise\":0.0},\"makeup\":{\"eyeMakeup\":true,\"lipMakeup\":true},\"hair\":{\"bald\":0.13,\"invisible\":false,\"hairColor\":[{\"color\":\"brown\",\"confidence\":0.99},{\"color\":\"black\",\"confidence\":0.52},{\"color\":\"red\",\"confidence\":0.39},{\"color\":\"blond\",\"confidence\":0.37},{\"color\":\"gray\",\"confidence\":0.15},{\"color\":\"other\",\"confidence\":0.13}]}}}]";

            var result = smileDetectionController.DetectSmile(json);

            Assert.AreEqual(SmileType.Happy, result);
        }

        [Test]
        public void ReturnVeryHappyWhenVeryHappySmileGiven()
        {
            const string json =
                "[{\"faceRectangle\":{\"top\":325,\"left\":104,\"width\":398,\"height\":398},\"faceAttributes\":{\"smile\":0.83,\"headPose\":{\"pitch\":-0.1,\"roll\":-2.8,\"yaw\":-22.1},\"gender\":\"female\",\"age\":20.0,\"facialHair\":{\"moustache\":0.0,\"beard\":0.0,\"sideburns\":0.0},\"glasses\":\"ReadingGlasses\",\"emotion\":{\"anger\":0.0,\"contempt\":0.0,\"disgust\":0.0,\"fear\":0.0,\"happiness\":1.0,\"neutral\":0.0,\"sadness\":0.0,\"surprise\":0.0},\"makeup\":{\"eyeMakeup\":true,\"lipMakeup\":true},\"hair\":{\"bald\":0.13,\"invisible\":false,\"hairColor\":[{\"color\":\"brown\",\"confidence\":0.99},{\"color\":\"black\",\"confidence\":0.52},{\"color\":\"red\",\"confidence\":0.39},{\"color\":\"blond\",\"confidence\":0.37},{\"color\":\"gray\",\"confidence\":0.15},{\"color\":\"other\",\"confidence\":0.13}]}}}]";

            var result = smileDetectionController.DetectSmile(json);

            Assert.AreEqual(SmileType.VeryHappy, result);
        }
    }
}