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
            int iQuotationIndex;

            strLineMod = strLineMod.Substring(iFindNodeNameLength, strLineMod.Length - iFindNodeNameLength);
            iQuotationIndex = strLineMod.IndexOf("\"");
            strBoneName = strLineMod.Substring(0, iQuotationIndex);

            return strBoneName;
        }


        //Begin Main
        private void buttonMakeSCLP_Click(object sender, EventArgs e)
        {

            #region
            List<string> listSourceASE = File.ReadLines(strSourcePath).ToList();
            List<string> listModASE = File.ReadLines(strModPath).ToList();

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
            for (iCounter = 0; iCounter <= (listSourceASE.Count - 1); iCounter++)
            {
                if (listSourceASE[iCounter].Contains(strFindGeomObject))
                {
                    listiGeomObjectIndex.Add(iCounter + 1);
                    listiNodeNameIndex.Add(iCounter + 2);
                    listiNodeTMIndex.Add(iCounter + 3);
                    listiTMScaleIndex.Add(iCounter + 15);
                    iNumBones++;
                }
            }


            //Get indexes of unchanged bones
            for (iCounter = 0; iCounter <= (iNumBones - 1); iCounter++)
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
            string[] arraystrBoneXYZ = new string[3];
            string strBoneName;

            string strLineTMScale;
            string strLineNodeName;

            for (iCounter = 0; iCounter <= (iNumBones - 1); iCounter++)
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


                //Bone data is finished, now we add it to the array
                arrayaseboneMod[iCounter] = aseboneTemp;
                //Et voila
            }



        }
        //End


    }

}
