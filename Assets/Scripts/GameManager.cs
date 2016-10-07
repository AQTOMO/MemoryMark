using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	int score = 0;

	public enum State {
		Display,
		Input,
	}

	Game game;

	State state = State.Display;

	bool waiting = false;

	// Use this for initialization
	void Start () {
		StartNewGame();
	}
	
	// Update is called once per frame
	void Update () {
		if (state != State.Input)
			return;

		CheckInput();
	}

	void StartNewGame() {
		state = State.Display;
	}

	public void DisplayMark() {
		// TODO マークを表示する
		if (game.HasNextMark()) {
			//uiManager.RecieveMark(game.CurrentMark);
			game.SetNextMark();
		} else {
			state = State.Input;
		}
	}

	void CheckInput() {
		if (!IsEnableInput())
			return;

		Game.Result result = game.SendInput(GetInputMakrk());

		if (result == Game.Result.Complete) {
			StartNewGame();
		} else if (result == Game.Result.Success) {
			score++;
		}
	}

	bool IsEnableInput() {
		return Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.D); 
	}

	Mark GetInputMakrk() {
		if (Input.GetKeyDown(KeyCode.K)) {
			return Mark.Circle;
		} else if (Input.GetKeyDown(KeyCode.J)) {
			return Mark.Cross;
		} else if (Input.GetKeyDown(KeyCode.F)) {
			return Mark.Square;
		} else if (Input.GetKeyDown(KeyCode.D)) {
			return Mark.Triangle;
		}
		throw new System.ArgumentException();
	}

}


public class Game {
	static int highScore;

	int markCount;
	List<Mark> marks;
	public Mark CurrentMark {
		get {
			return marks[markCount];
		}
	}

	Difficulty difficulty;

	public enum Result {
		Success,
		Fail,
		Complete,
	}


	public Game(Difficulty difficulty, int level = 0) {
		markCount = 0;
		this.difficulty = difficulty;
		marks = GenereateRandomMarks(difficulty);
	}

	public List<Mark> GenereateRandomMarks(Difficulty difficulty) {
		// TODO 問題生成実装方法を考える
		return null;
	}

	public Result SendInput(Mark inputMark) {
		if (!CheckMarkCorrection(inputMark))
			return Result.Fail;

		markCount++;
		if (markCount > marks.Count)
			return Result.Complete;
		return Result.Success;
	}

	public bool CheckMarkCorrection(Mark inputMark) {
		return marks[markCount] == inputMark;
	}

	public void SetNextMark() {
		markCount++;
	}

	public bool HasNextMark() {
		return markCount < marks.Count;
	}

}


