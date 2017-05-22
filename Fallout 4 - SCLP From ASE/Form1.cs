using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Fallout_4___SCLP_From_ASE
{
    public partial class windowProgram : Form
    {
        //Begin variables setup
        public class classASEBone
        {
            string _NodeName = new string("Null".ToCharArray());
            classASENodeTM _ASENodeTM = new classASENodeTM();

            public string NodeName
            {
                get { return _NodeName; }
                set { _NodeName = value; }
            }

            public classASENodeTM ASENodeTM
            {
                get { return _ASENodeTM; }
                set { _ASENodeTM = value; }
            }

        }

        public class classASENodeTM
        {
            string _strTMScaleX = new string("Null".ToCharArray());
            string _strTMScaleY = new string("Null".ToCharArray());
            string _strTMScaleZ = new string("Null".ToCharArray());

            public string strTMScaleX
            {
                get { return _strTMScaleX; }
                set { _strTMScaleX = value; }
            }

            public string strTMScaleY
            {
                get { return _strTMScaleY; }
                set { _strTMScaleY = value; }
            }

            public string strTMScaleZ
            {
                get { return _strTMScaleZ; }
                set { _strTMScaleZ = value; }
            }

        }

        public string strSourcePath;
        public string strModPath;
        public static string strFindGeomObject = "*GEOMOBJECT {";
        public static string strFindNodeName = "\t" + "*NODE_NAME " + "\"";
        public static string strFindTMScale = "\t" + "\t" + "*TM_SCALE ";

        public int iFindTMScaleLength = strFindTMScale.Length;
        public int iFindNodeNameLength = strFindNodeName.Length;
        //End

        #region
        public windowProgram()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void buttonSourcePath_Click(object sender, EventArgs e)
        {

            if (dialogSourcePath.ShowDialog() == DialogResult.OK)
            {
                strSourcePath = dialogSourcePath.FileName;
                textboxSourcePath.Text = strSourcePath;

                if (File.Exists(textboxModPath.Text))
                {
                    buttonMakeSCLP.Enabled = true;
                }

            }

        }

        private void buttonModPath_Click(object sender, EventArgs e)
        {

            if (dialogModPath.ShowDialog() == DialogResult.OK)
            {
                strModPath = dialogModPath.FileName;
                textboxModPath.Text = strModPath;

                if (File.Exists(textboxSourcePath.Text))
                {
                    buttonMakeSCLP.Enabled = true;
                }

            }

        }
        #endregion


        private string[] GetScalingXYZ(string strLine)
        {
            string[] arraystrXYZ = new string[3];
            string strLineMod = strLine;
            string strCurrentValue;

            int iTabIndex;



            strLineMod = strLineMod.Substring(iFindTMScaleLength, (strLineMod.Length - iFindTMScaleLength));
            iTabIndex = strLineMod.IndexOf("\t");
            strCurrentValue = strLineMod.Substring(0, iTabIndex);
            arraystrXYZ[0] = strCurrentValue;

            strLineMod = strLineMod.Substring(iTabIndex + 1, strLineMod.Length - (iTabIndex + 1));
            iTabIndex = strLineMod.IndexOf("\t");
            strCurrentValue = strLineMod.Substring(0, iTabIndex);
            arraystrXYZ[1] = strCurrentValue;

            strLineMod = strLineMod.Substring(iTabIndex + 1, strLineMod.Length - (iTabIndex + 1));
            strCurrentValue = strLineMod.Substring(0, strLineMod.Length);
            arraystrXYZ[2] = strCurrentValue;


            return arraystrXYZ;
        }


        private string GetBoneName(string strLine)
        {
            string strBoneName;
            string strLineMod = strLine;

            //MessageBox.Show(strLineMod);
            strLineMod = strLineMod.Substring(iFindNodeNameLength, strLineMod.Length - iFindNodeNameLength);
            //MessageBox.Show(strLineMod);
            strBoneName = strLineMod.Substring(0, strLineMod.Length - 1);
            //MessageBox.Show("Bone Name Is: "+strBoneName);

            return strBoneName;
        }


        //Begin Main
        private void buttonMakeSCLP_Click(object sender, EventArgs e)
        {
            if (!(File.Exists(textboxModPath.Text)) || !(File.Exists(textboxSourcePath.Text)))
            {
                MessageBox.Show("Your ASE file paths are invalid!");
                return;
            }

            #region
            List<string> listSourceASE = File.ReadLines(strSourcePath).ToList();
            List<string> listModASE = File.ReadLines(strModPath).ToList();
            List<string> listBoneData = new List<string>();

            List<int> listiGeomObjectIndex = new List<int>();
            List<int> listiNodeNameIndex = new List<int>();
            List<int> listiNodeTMIndex = new List<int>();
            List<int> listiTMScaleIndex = new List<int>();
            List<int> listiDuplicateBoneIndex = new List<int>();

            int iNumBones = 0;
            #endregion

            int iCounter;

            //Get the indexes of the bone structures' data
            #region
            for (iCounter = 0; iCounter < listSourceASE.Count - 1; iCounter++)
            {
                if (listSourceASE[iCounter].Contains(strFindGeomObject))
                {
                    listiGeomObjectIndex.Add(iCounter);
                    listiNodeNameIndex.Add(iCounter + 1);
                    listiNodeTMIndex.Add(iCounter + 3);
                    listiTMScaleIndex.Add(iCounter + 15);
                    iNumBones++;
                    iCounter = iCounter + 15;
                }
            }


            //Get indexes of unchanged bones
            for (iCounter = 0; iCounter < iNumBones; iCounter++)
            {
                if (listSourceASE[listiTMScaleIndex[iCounter]] == listModASE[listiTMScaleIndex[iCounter]])
                {
                    listiDuplicateBoneIndex.Add(listiGeomObjectIndex[iCounter]);
                }
            }

            //Remove unchanged bone indexes from lists
            foreach (int iIndex in listiDuplicateBoneIndex)
            {

                for (iCounter = (iNumBones - 1); iCounter >= 0; iCounter--)
                {
                    if (iIndex == listiGeomObjectIndex[iCounter])
                    {
                        listiGeomObjectIndex.RemoveAt(iCounter);
                        listiNodeNameIndex.RemoveAt(iCounter);
                        listiNodeTMIndex.RemoveAt(iCounter);
                        listiTMScaleIndex.RemoveAt(iCounter);
                        iNumBones--;
                    }
                }

            }
            #endregion



            //Create arrays for all the modified bones
            classASEBone[] arrayaseboneMod = new classASEBone[iNumBones];
            for (iCounter = 0; iCounter < iNumBones; iCounter++)
            {
                arrayaseboneMod[iCounter] = new classASEBone();
            }

            string[] arraystrBoneXYZ = new string[3];
            string strBoneName;

            string strLineTMScale;
            string strLineNodeName;

            #region
            for (iCounter = 0; iCounter < iNumBones; iCounter++)
            {
                classASEBone aseboneTemp = arrayaseboneMod[iCounter];

                classASENodeTM asenodeTemp = new classASENodeTM();


                //Get Bone name
                strLineNodeName = listModASE[listiNodeNameIndex[iCounter]];
                strBoneName = GetBoneName(strLineNodeName);
                //end

                strLineTMScale = listModASE[listiTMScaleIndex[iCounter]];
                arraystrBoneXYZ = GetScalingXYZ(strLineTMScale);


                //Create the bone data
                aseboneTemp.NodeName = strBoneName;

                asenodeTemp.strTMScaleX = arraystrBoneXYZ[0];
                asenodeTemp.strTMScaleY = arraystrBoneXYZ[1];
                asenodeTemp.strTMScaleZ = arraystrBoneXYZ[2];

                aseboneTemp.ASENodeTM = asenodeTemp;
                //end

                //MessageBox.Show(strBoneName);
                //MessageBox.Show("X - " + arraystrBoneXYZ[0] + " Y - " + arraystrBoneXYZ[1] + " Z - " + arraystrBoneXYZ[2]);

                //Bone data is finished, now we add it to the array
                arrayaseboneMod[iCounter] = aseboneTemp;
                //Et voila
            }
            #endregion


            //Now we create the text list in json format
            #region
            listBoneData.Add("[");

            for (iCounter = 0; iCounter < iNumBones; iCounter++)
            {
                strBoneName = arrayaseboneMod[iCounter].NodeName;

                listBoneData.Add("  " + "{");
                listBoneData.Add("    " + "\"Name\": \"" + strBoneName + "\",");


                listBoneData.Add("    " + "\"Scale\": {");

                listBoneData.Add("      " + "\"x\": " + arrayaseboneMod[iCounter].ASENodeTM.strTMScaleX + ",");
                listBoneData.Add("      " + "\"y\": " + arrayaseboneMod[iCounter].ASENodeTM.strTMScaleY + ",");
                listBoneData.Add("      " + "\"z\": " + arrayaseboneMod[iCounter].ASENodeTM.strTMScaleZ);

                listBoneData.Add("    }");


                if (iCounter == (iNumBones - 1))
                {
                    listBoneData.Add("  }");
                }

                else
                {
                    listBoneData.Add("  },");
                }


            }

            listBoneData.Add("]");
            #endregion


            //Finally, output listBoneData as our .sclp file!
            string strFileName;

            if (dialogMakeSCLP.ShowDialog() == DialogResult.OK)
            {
                strFileName = dialogMakeSCLP.FileName;
                System.IO.File.WriteAllLines(strFileName, listBoneData);
            }
            //End


        }
        //End


    }

}
