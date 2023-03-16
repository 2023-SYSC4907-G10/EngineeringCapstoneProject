using System.Collections.Generic;
public class TutorialInit
{
    public static void DefenseMenu()
    {
        List<string> tutorialImages = new List<string> { "Tutorial/Images/DefenseMenu1", "Tutorial/Images/DefenseMenu2", "Tutorial/Images/DefenseMenu3" };
        TutorialSingleton.GetInstance().setTextFile("Tutorial/Text/DefenseTutorial");
        TutorialSingleton.GetInstance().setTutorialImages(tutorialImages);
        TutorialSingleton.GetInstance().setEndTutorialTransitionScene("DefenseMenu");
    }

    public static void AttackMenu()
    {
        List<string> tutorialImages = new List<string> { "Tutorial/Images/AttackMenu1", "Tutorial/Images/AttackMenu2", "Tutorial/Images/AttackMenu1" };
        TutorialSingleton.GetInstance().setTextFile("Tutorial/Text/AttackTutorial");
        TutorialSingleton.GetInstance().setTutorialImages(tutorialImages);
        TutorialSingleton.GetInstance().setEndTutorialTransitionScene("AttackMenu");
    }

    public static void AttackMinigame(SecurityConcepts sc) {
        switch(sc) {
            case SecurityConcepts.InsiderAttack:
                InsiderAttack();
                break;
            case SecurityConcepts.DDoS:
                DDosAttack();
                break;
            case SecurityConcepts.Ransomware:
                RansomwareAttack();
                break;
            case SecurityConcepts.Phishing:
                PhisingAttack();
                break;
            case SecurityConcepts.Firewall:
                FirewallAttack();
                break;
        }
    }

    public static void DefenseMinigame(SecurityConcepts sc) {
        switch(sc) {
            case SecurityConcepts.InsiderAttack:
                InsiderDefense();
                break;
            case SecurityConcepts.DDoS:
                DDosDefense();
                break;
            case SecurityConcepts.Ransomware:
                RansomwareDefense();
                break;
            case SecurityConcepts.Phishing:
                PhisingDefense();
                break;
            case SecurityConcepts.Firewall:
                FirewallDefense();
                break;
        }
    }

    private static void InsiderAttack()
    {
        List<string> tutorialImages = new List<string> { "Tutorial/Images/InsiderAttack1", "Tutorial/Images/InsiderAttack2", "Tutorial/Images/InsiderAttack3", "Tutorial/Images/InsiderAttack1" };
        TutorialSingleton.GetInstance().setTextFile("Tutorial/Text/InsiderAttack");
        TutorialSingleton.GetInstance().setTutorialImages(tutorialImages);
        TutorialSingleton.GetInstance().setEndTutorialTransitionScene("InsiderAttack_Attack");
    }

    private static void DDosAttack()
    {
        List<string> tutorialImages = new List<string> { "Tutorial/Images/DDosAttack1", "Tutorial/Images/DDosAttack2", "Tutorial/Images/DDosAttack3" };
        TutorialSingleton.GetInstance().setTextFile("Tutorial/Text/DdosAttack");
        TutorialSingleton.GetInstance().setTutorialImages(tutorialImages);
        TutorialSingleton.GetInstance().setEndTutorialTransitionScene("DDoS_Attack");
    }

    private static void RansomwareAttack()
    {
        List<string> tutorialImages = new List<string> { "Tutorial/Images/RansomwareAttack1", "Tutorial/Images/RansomwareAttack2", "Tutorial/Images/RansomwareAttack3" };
        TutorialSingleton.GetInstance().setTextFile("Tutorial/Text/RansomwareAttack");
        TutorialSingleton.GetInstance().setTutorialImages(tutorialImages);
        TutorialSingleton.GetInstance().setEndTutorialTransitionScene("Ransomware_Attack");
    }

    private static void PhisingAttack()
    {
        List<string> tutorialImages = new List<string> { "Tutorial/Images/PhisingAttack1", "Tutorial/Images/PhisingAttack2", "Tutorial/Images/PhisingAttack3" };
        TutorialSingleton.GetInstance().setTextFile("Tutorial/Text/PhisingAttack");
        TutorialSingleton.GetInstance().setTutorialImages(tutorialImages);
        TutorialSingleton.GetInstance().setEndTutorialTransitionScene("Phishing_Attack");
    }

    private static void FirewallAttack()
    {
        List<string> tutorialImages = new List<string> { "Tutorial/Images/FirewallAttack1", "Tutorial/Images/FirewallAttack2", "Tutorial/Images/FirewallAttack3" };
        TutorialSingleton.GetInstance().setTextFile("Tutorial/Text/FirewallAttack");
        TutorialSingleton.GetInstance().setTutorialImages(tutorialImages);
        TutorialSingleton.GetInstance().setEndTutorialTransitionScene("Firewall_Attack");
    }

    private static void InsiderDefense()
    {
        List<string> tutorialImages = new List<string> { "Tutorial/Images/InsiderDefense1", "Tutorial/Images/InsiderDefense2", "Tutorial/Images/InsiderDefense3", "Tutorial/Images/InsiderDefense4" };
        TutorialSingleton.GetInstance().setTextFile("Tutorial/Text/InsiderDefense");
        TutorialSingleton.GetInstance().setTutorialImages(tutorialImages);
        TutorialSingleton.GetInstance().setEndTutorialTransitionScene("InsiderAttack_Defense");
    }

    private static void DDosDefense()
    {
        List<string> tutorialImages = new List<string> { "Tutorial/Images/DDosDefense1", "Tutorial/Images/DDosDefense2", "Tutorial/Images/DDosDefense3" };
        TutorialSingleton.GetInstance().setTextFile("Tutorial/Text/DDosDefense");
        TutorialSingleton.GetInstance().setTutorialImages(tutorialImages);
        TutorialSingleton.GetInstance().setEndTutorialTransitionScene("DDoS_Defense");
    }

    private static void RansomwareDefense()
    {
        List<string> tutorialImages = new List<string> { "Tutorial/Images/RansomwareDefense1", "Tutorial/Images/RansomwareDefense2", "Tutorial/Images/RansomwareDefense3" };
        TutorialSingleton.GetInstance().setTextFile("Tutorial/Text/RansomwareDefense");
        TutorialSingleton.GetInstance().setTutorialImages(tutorialImages);
        TutorialSingleton.GetInstance().setEndTutorialTransitionScene("Ransomware_Defense");
    }

    private static void PhisingDefense()
    {
        List<string> tutorialImages = new List<string> { "Tutorial/Images/PhisingDefense1", "Tutorial/Images/PhisingDefense2", "Tutorial/Images/PhisingDefense3", "Tutorial/Images/PhisingDefense4", "Tutorial/Images/PhisingDefense5" };
        TutorialSingleton.GetInstance().setTextFile("Tutorial/Text/PhisingDefense");
        TutorialSingleton.GetInstance().setTutorialImages(tutorialImages);
        TutorialSingleton.GetInstance().setEndTutorialTransitionScene("Phishing_Defense");
    }

    private static void FirewallDefense()
    {
        List<string> tutorialImages = new List<string> { "Tutorial/Images/FirewallDefense1", "Tutorial/Images/FirewallDefense2", "Tutorial/Images/FirewallDefense3" };
        TutorialSingleton.GetInstance().setTextFile("Tutorial/Text/FirewallDefense");
        TutorialSingleton.GetInstance().setTutorialImages(tutorialImages);
        TutorialSingleton.GetInstance().setEndTutorialTransitionScene("Firewall_Defense");
    }
}