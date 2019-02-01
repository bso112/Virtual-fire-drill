using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using GoogleARCore;
using UnityEngine.UI;

namespace notUsed
{


    public sealed class GameManager : MonoBehaviour
    {

        public static GameManager instance = null;



        //인스펙터에서 넣어주셈 
        [HideInInspector] public GameObject[] items;
        [HideInInspector] public GameObject[] itemImgs;
        public GameObject exthinguisher, towel, sand, electricPad, multiTap, gasValve, alaram, elevator;
        [HideInInspector] public GameObject exthinguisherImg, towelImg, sandImg, electricPadImg, multiTapImg, gasValveImg, alaramImg, elevatorImg;
        VideoPlayer videoPlayer;
        AudioSource audioSource;
        public VideoClip exthinguisherVideo, elevatorVideo, towelVideo;
        public AudioClip alertingSound;
        public GameObject fire, extinguisherEffect, sandEffect; // 파티클시스템 프리펩을 넣어줘야함

        public Text scoreText;
        [HideInInspector] public int score;


        [HideInInspector] public string itemName = null;
        public Text consol;



        private bool touchOn;

        string target = null;








        //IEnumerator FireCreate;

        //IEnumerator FireCreate_routin() //불을 지피는 코루틴
        //{


        //    //불을 지핀다
        //    // 화면 잡힌것들중에서 원하는 것을 가져옴
        //    // Session.GetTrackables<Type>();

        //    // 화면 터치를 최소 한 지점이라도 해야 계속 진행
        //    if (Input.touchCount <= 0)
        //    {
        //        yield break;
        //    }

        //    Touch touchPosition = Input.GetTouch(0);

        //    // Frame Raycast에 의한 충돌 정보 + 기타 등등 정보를 담는 단순 컨테이너
        //    TrackableHit hit;
        //    // Frame.Raycast(스크린 x, 스크린 y, 필터링할것들, 정보를 담을 컨테이너)
        //    // Feature point 특정 사물
        //    if (Frame.Raycast(touchPosition.position.x, touchPosition.position.y, TrackableHitFlags.PlaneWithinInfinity, out hit))
        //    {
        //        // 감지된 바닥이 맞다면
        //        if (hit.Trackable is DetectedPlane)
        //        {

        //            Instantiate(fire, hit.Pose.position, hit.Pose.rotation);

        //        }
        //    }
        //    yield return null;



        //}







        public enum ItemType
        {
            Equipment,//장비
            Consumption,//소모
            Misc//기타
        }

        public struct ItemData
        {
            public ItemSort name;
            public ItemType type;


            public ItemData(ItemSort name, ItemType type)
            {
                this.name = name;
                this.type = type;

            }


        };

        private ItemData[] itemDatas = new ItemData[]
           {
            new ItemData(ItemSort.exthinguisher, ItemType.Equipment),
            new ItemData(ItemSort.towel, ItemType.Equipment),
            new ItemData(ItemSort.sand, ItemType.Consumption),
            new ItemData(ItemSort.electricPad, ItemType.Misc),
            new ItemData(ItemSort.multiTap, ItemType.Misc),
            new ItemData(ItemSort.gasValve, ItemType.Misc),
            new ItemData(ItemSort.alarm, ItemType.Misc),

           };


        public enum ItemSort { none = -1, exthinguisher = 0, towel, sand, electricPad, multiTap, gasValve, alarm, elevator, count };


        private void Awake()
        {
            // FireCreate = FireCreate_routin();
            //Check if instance already exists
            if (instance == null)
            {
                //if not, set instance to this
                instance = this;
            }
            //If instance already exists and it's not this:
            else if (instance != this)
            {
                //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
                Destroy(gameObject);
            }

            //Sets this to not be destroyed when reloading scene
            DontDestroyOnLoad(gameObject);
        }


        // Start is called before the first frame update
        void Start()
        {

            touchOn = false;
            itemImgs = GameObject.FindGameObjectsWithTag("itemImg");
            for (int i = 0; i < itemImgs.Length; i++)
            {
                switch (itemImgs[i].name)
                {
                    case "exthinguisherImg":
                        exthinguisherImg = itemImgs[i];
                        break;
                    case "towelImg":
                        towelImg = itemImgs[i];
                        break;
                    case "sandImg":
                        sandImg = itemImgs[i];
                        break;
                    case "electricPadImg":
                        electricPadImg = itemImgs[i];
                        break;
                    case "multiTapImg":
                        multiTapImg = itemImgs[i];
                        break;
                    case "gasValveImg":
                        gasValveImg = itemImgs[i];
                        break;
                    case "alaramImg":
                        alaramImg = itemImgs[i];
                        break;
                    case "elevatorImg":
                        elevatorImg = itemImgs[i];
                        break;

                }
            }
            // StartCoroutine(FireCreate);

            videoPlayer = gameObject.GetComponent<VideoPlayer>();
            audioSource = gameObject.GetComponent<AudioSource>();

            items = GameObject.FindGameObjectsWithTag("item");
            for (int i = 0; i < items.Length; i++)
            {
                switch (items[i].name)
                {
                    case "exthinguisher":
                        exthinguisher = items[i];
                        break;
                    case "towel":
                        towel = items[i];
                        break;
                    case "sand":
                        sand = items[i];
                        break;
                    case "electricPad":
                        electricPad = items[i];
                        break;
                    case "multiTap":
                        multiTap = items[i];
                        break;
                    case "gasValve":
                        gasValve = items[i];
                        break;
                    case "alaram":
                        alaram = items[i];
                        break;
                    case "elevator":
                        elevator = items[i];
                        break;

                }
            }


        }


        private void Update() //클릭을 받아 오브젝트 이름을 this.itemName에 넣음
        {

            scoreText.text = "SCORE :" + score;

        }



        public void PickUp() //오브젝트 줍기
        {


            Item rstItem = exthinguisher.GetComponent<Item>(); //여기 원래 null임. 디버그를 위해

            switch (this.itemName)
            {

                case "exthinguisherImg":
                    if (rstItem == null) //equal로 해야하나?
                    {
                        Debug.Log("rstItem이 null입니다");
                    }
                    rstItem = exthinguisher.GetComponent<Item>();
                    break;
                case "sandImg":
                    rstItem = sand.GetComponent<Item>();
                    break;
                    //case ItemSort.elevator:
                    //    break;
                    //case ItemSort.gasValve:
                    //    break;
                    //case ItemSort.multiTap:
                    //    break;
                    //case ItemSort.sand:
                    //    break;
                    //case ItemSort.towel:
                    //    break;
                    //case ItemSort.alarm:
                    //    break;

            }




        }





        public void Info()
        {
            switch (this.itemName)
            {

                case "exthinguisherImg":
                    Debug.Log("GameManger>Info>extinguisherImg");
                    videoPlayer.clip = exthinguisherVideo; //소화기 사용법 비디오 틀기
                    videoPlayer.Play();
                    break;
                case "sandImg":
                    Debug.Log("GameManger>Info>extinguisherImg");
                    break;
                    //case ItemSort.elevator:
                    //    break;
                    //case ItemSort.gasValve:
                    //    break;
                    //case ItemSort.multiTap:
                    //    break;
                    //case ItemSort.sand:
                    //    break;
                    //case ItemSort.towel:
                    //    break;
                    //case ItemSort.alarm:
                    //    break;

            }

        }



        public void Use_Item(string itemName)  //아이템을 사용할 시 발생되는 이벤트
        {
            //StopCoroutine(FireCreate); //클릭하면 불이 생성되는 코루틴 정지
            switch (this.itemName)
            {
                case "exthinguisher":
                    Jet(extinguisherEffect); //클릭하면 소화기 분사
                    break;
                case "gasValve":
                    float smooth = 1f;
                    Quaternion target = Quaternion.Euler(90, 0, 0);  //x축으로 90도 돌리기
                    gasValve.transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
                    this.score += 100;
                    break;
                case "alarm":
                    audioSource.clip = alertingSound; //비상벨 울리기
                    audioSource.Play();
                    this.score += 100;
                    break;
                case "sand":
                    Jet(sandEffect); //클릭하면 모래분사
                    break;
                case "towel":
                    //videoPlayer.clip = towelVideo; 이것들은 연기가 났을때 실행
                    //videoPlayer.Play();
                    this.score += 100;
                    break;
                case "elevator":
                    videoPlayer.clip = elevatorVideo;
                    videoPlayer.Play();
                    this.score -= 200;
                    break;



            }
            //StartCoroutine(FireCreate);

        }

        public void Jet(GameObject effect)
        {

            Debug.Log("jet!");
            int fireCount = 0;
            if (Input.touchCount <= 0) //터치가 없으면 아무것도 안함
            {
                return;
            }
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos;
            Ray ray;
            RaycastHit hit;
            Vector3 touchPosToVector3 = new Vector3(touch.position.x, touch.position.y, 100);
            touchPos = Camera.main.ScreenToWorldPoint(touchPosToVector3);
            ray = Camera.main.ScreenPointToRay(touchPosToVector3);


            GameObject _effect = Instantiate(effect) as GameObject; // 터치한 위치에 이펙트 넣기
            _effect.transform.position = touchPos;
            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.red, 1.5f);

                if (hit.collider.tag == "fire") //불의 태그를 fire로 해야함.
                {
                    Debug.Log("fire 터치 ! ");
                    float alpha = 1;
                    Renderer renderer = fire.GetComponent<Renderer>();
                    renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, alpha);
                    while (!(alpha < 0))
                    {
                        alpha -= Time.deltaTime;
                    }
                    Destroy(fire);
                    fireCount++;
                    if (fireCount > 10)
                    {
                        this.score += 200; //200점 올린다.
                        return;
                    }


                }
            }
        }


    }
}

