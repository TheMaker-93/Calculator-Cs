using System;

namespace Calculadora
{

	public class Calculator
	{
		// Private constant to define the maximim size of the arrays storing the calculator operations
		private const int MAX_MEMORY = 10;

		// Class attributes
		private char[] _operations;             // Array storing the operations each of them as chars ('+', '-', '/', '*')
		private int[] _operands1;               // Array storing the first operand for an operation
		private int[] _operands2;               // Array storing the second operand for an operation
		public int _numRecordedOperations;     // Counter of the number of operations introduced

		// Default constructor
		public Calculator()
		{
			// Initialize the class attributes
			_operations = new char[MAX_MEMORY];                         // inicializamos el array de operatdores
			_operands1 = new int[MAX_MEMORY];                           // ARRAY que guardara los operadores1 de las 10 operaciones
			_operands2 = new int[MAX_MEMORY];                           // ARRAY que guardara los operadores2 de las 10 operaciones
			_numRecordedOperations = 0;                                 // contador de numero de operaciones
		}

		// Public methods of the class
		public void addOperation(char operation, int operand1, int operand2)            
		{

			// solo si el operador es correcto se guardara la operacion en los arrays
			if (operation == '+' || operation == '-' || operation == '/' || operation == '*')
			{

				// si llegamos al maximo de memoria...
				if (_numRecordedOperations == MAX_MEMORY)
				{
					deleteOperation(0); // borramos el valor en la primera posicion
										//_numRecordedOperations--;
				}

				_operations[_numRecordedOperations] = operation;
				_operands1[_numRecordedOperations] = operand1;         
				_operands2[_numRecordedOperations] = operand2;

				_numRecordedOperations++;   // añadimos 1 al contador de operaciones guardadas	

			}
			else {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("The operation sign you have introduced is not valid");
				Console.ForegroundColor = ConsoleColor.Yellow;

			}

		}

		// Private class methods
		public void deleteOperation(int position)                   // TODO DANI CREO QUE AQUI HAY UN BUG  
		{
			// Delete the operation in position "position" in the calculator (arrays + counter)


			// nos aseguramos que la posicion introducida este dentro del rango 0 a la posicion mas alta introducida
			if (position < _numRecordedOperations && position >= 0 && position < MAX_MEMORY)
			{

				for (int i = position; i < _numRecordedOperations - 1; i++)     // desde la posicion (0 a 9) a la cantidad de operaciones echas -2
				{
					// este bucle empezara en la posicion indicada y borrara el valor de los tres arrays en esa posicion y 
					// desplazara el resto una posicion hacia atras

					_operands1[i] = _operands1[i + 1];
					_operands2[i] = _operands2[i + 1];
					_operations[i] = _operations[i + 1];

				}

				_numRecordedOperations--;

			}
			else {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Invalid position: there is nothing to erase there");
				Console.ForegroundColor = ConsoleColor.Yellow;
			}




		}

		public int evalOperation(int operationPos)                          //TODO DONE DANI 
		{
			// Evaluate the operation stored in the arrays of the class in the position "operationPos"
			// Validate that "operationPos" is a valid operation position (user can ask for a position with no current operation stored).

			int result = 0;

			// nos aseguramos que la operacion llamada a evaluar este dentro del rango de operaciones existente y no en un espacio vacio
			if (operationPos < _numRecordedOperations && operationPos >= 0 && operationPos < MAX_MEMORY)
			{
				// calculamos
				switch (_operations[operationPos])
				{
					case '+':
						result = _operands1[operationPos] + _operands2[operationPos];
						break;
					case '-':
						result = _operands1[operationPos] - _operands2[operationPos];
						break;
					case '*':
						result = _operands1[operationPos] * _operands2[operationPos];
						break;
					case '/':
						result = _operands1[operationPos] / _operands2[operationPos];
						break;
				}

			}
			else {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("There are no current operations to calculate in the specified position");
				Console.ForegroundColor = ConsoleColor.Yellow;
			}

			return result;
		}

		public void printOperation(int operationPos)                        //TODO IN PROGRESS DANI 
		{
			// Print the operation info from a given position/order from the recorded operations in the calculator arrays.
			// Validate "operationPos" is a valid operation position (user can ask for a position with no current operation stored).

			if (operationPos < _numRecordedOperations && operationPos >= 0 && operationPos < MAX_MEMORY)
			{

				Console.WriteLine(_operands1[operationPos] + " " + _operations[operationPos] + " " + _operands2[operationPos] + " = " + evalOperation(operationPos));

			}
			else {

				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("There are no operations to show in the specified position");
				Console.ForegroundColor = ConsoleColor.Yellow;
			}


		}

		public void printAllOperations()               		
		{
			// sino hay ninguna operacion hecha avisamos al usuario
			if (_numRecordedOperations == 0)            
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("There are not operations to show");


			}
			else {                                  // si la hay las mostramos

				// de la operacion 0 a la ultima grabada - 1 ya que en i empezamos en 0
				for (int i = 0; i < _numRecordedOperations; i++)    
				{
					printOperation(i);
				}
			}
		}

		public void printHigherResultOperation()                            // TODO DANI DON
		{
			// Print in the console the operation and operands info with the higher result from the ones stored in the arrays of the calculator
			// Take into account that there can be less than "MAX_MEMORY" operations introduced => class attribute "_numRecordedOperations"
			// This public method has to use the public method "evalOperation" method to evaluate each operation stored in the arrays

			int topValue = 0;
			int position = 0;


			if (_numRecordedOperations == 0)            // sino hay ninguna operacion hecha avisamos al usuario
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("There are not operations to show");
				Console.ForegroundColor = ConsoleColor.Yellow;
			}
			else {

				// bucle que ira desde la primera posicion de operaciones hasta la mayor (teniendo en cuenta que i empieza en 0 pero recorded operations en 1
				for (int i = 0; i < _numRecordedOperations; i++)
				{

					// en el caso que encontremos un valor superior al almacenado este pasara a ser el mayor
					if (evalOperation(i) > topValue)
					{
						topValue = evalOperation(i);
						position = i;                   // guardamos la posicion para expresar la operacion fuera del for
					}

				}

				// Expresamos el operacion con el valor superior usando la posicion almacenada anteriormente en el bucle
				Console.WriteLine(_operands1[position] + " " + _operations[position] + " " + _operands2[position] + " = " + evalOperation(position));
			}



		}


		public void printHigherResultOperation(char operationType)              // TODO DANI IN TESTING
		{
			// Print in the console the operation and operands info with the higher result from the ones stored in the arrays of the calculator of type "operationType" (Ex: higher result of '+').
			// "operationtype" parameter has to be one of the following chars '+', '-', '*' or '/'
			// Take into account that there can be less than "MAX_MEMORY" operations introduced => class attribute "_numRecordedOperations"
			// This public method has to use the public method "evalOperation" method to evaluate each operation stored in the arrays

			int topValue = 0;
			int position = 0;

			// sino encontramos valores que mostrar o computar avisamos al usuario
			if (_numRecordedOperations == 0)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("There are not operations to show");
				Console.ForegroundColor = ConsoleColor.Yellow;
			}
			// en el caso que si hayan entonces empezamos a computar
			else {

				// Comprovamos que el signo introducio es valido, es decir, es uno de los reconocidos
				// si tubiera mas usaria directamente un bucle por el array para hacer la comprovacion
				if (operationType == '+' || operationType == '-' || operationType == '/' || operationType == '*')
				{

					// bucle que pasara por todos los elementos del array ( los que tengan valor )
					// pero solo tendra en cuenta los del signo introducido a la hora de computar el mayor resultado
					for (int index = 0; index < _numRecordedOperations; index++)
					{

						if (_operations[index] == operationType && evalOperation(index) > topValue)
						{

							topValue = evalOperation(index);
							position = index;

						}
					}
				}


				// Expresamos el operacion con el valor superior usando la posicion almacenada anteriormente en el bucle
				// uso operationType porque para el sistema es algo mas rapido acceder a esa variable que a un indice de un vector ( o eso creo )
				// tambien podria poner topvalue al lado de la igualdad pero se me indica que use evalOperation asi que lo usare siempre que sea posible
				Console.WriteLine(_operands1[position] + " " + operationType + " " + _operands2[position] + " = " + evalOperation(position));

			}


		}

		public bool anyNegativeOperation()                                  // TODO DANI DONE 
		{
			// Returns true if there is any operation in the memory of the calculator (arrays) which its result is negative
			// This public method has to use the public method "evalOperation" method to evaluate each operation stored in the arrays


			bool negativeOperations = false;                // no necesito esta variable pero la uso porque se me da junto con el metodo

			for (int position = 0; /*position < MAX_MEMORY*/ /* 0 to 9*/ /*&&*/ position < _numRecordedOperations; position++)
			{

				if (evalOperation(position) < 0)            // si el resultado de alguna operacion es negativo entonces decimos que si que hay resultados negativos
				{
					negativeOperations = true;
					return negativeOperations;              // detenemos el bucle ya que no tiene sentido seguir ejecutandolo si ya sabemos que hay algun resultado negativo
				}

			}

			return negativeOperations;              // en el caso que no se cumpla la condicion mandaremos el "resultado" al acabar el bucle

		}
	}

	// Main class
	class MainClass
	{
		public static void Main(string[] args)
		{


			// Inicializacion del constructor
			Calculator calc = new Calculator();             // cargamos el constructor


			int operand1, operand2, position;
			char operation;
			string yesNo;
			bool keepCalc, keepCalcQuestion;
			int choice;

			operand1 = 0;
			operand2 = 0;
			position = 0;
			keepCalc = false;
			keepCalcQuestion = true;
			string numeroStr;
			choice = 0;


			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("Welcome to ");
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("Mario's ");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("&");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write(" Dani's ");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("Calculator!");
			Console.ResetColor();

			Console.WriteLine();


			while (keepCalc == false) // Quiero seguir usando la calculadora?
			{
				keepCalcQuestion = true;

				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine("Make a choice between these options:");


				Console.WriteLine("\n\n\t\t1. Add operation\n\t\t2. Delete operation\n\t\t3. Evaluate operation\n\t\t4. Print an operation at a given position\n\t\t5. Print all operations\n\t\t6. Print operation with highest result\n\t\t7. Print an specific (+, -, *, /) operation with highest result\n\t\t8. Any negative values?\n\t\t9. EXIT");

				Console.WriteLine();



				Console.Write("Write a number between 1 and 9: ");
				numeroStr = Console.ReadLine();

				if (!checkIsInRange(numeroStr))
				{

					choice = Convert.ToInt32(numeroStr);


				}

				while (checkIsInRange(numeroStr) || choice < 1 || choice > 9)
				{

					Console.Write("Write a number between 1 and 9: ");
					numeroStr = Console.ReadLine();

					if (!checkIsInRange(numeroStr))
					{

						choice = Convert.ToInt32(numeroStr);

					}
				}

				switch (choice)
				{
					case 1://Hace una operación

						Console.Clear();

						Console.Write("Give me the first number: ");
						numeroStr = Console.ReadLine();

						if (!checkIsInRange(numeroStr))
						{

							operand1 = Convert.ToInt32(numeroStr);


						}

						while (checkIsInRange(numeroStr))
						{

							Console.Write("Give me the first number: ");
							numeroStr = Console.ReadLine();

							if (!checkIsInRange(numeroStr))
							{

								operand1 = Convert.ToInt32(numeroStr);


							}


						}

						Console.Write("What operation do you want?('+' , '-' , '*' , '/'): ");
						operation = Convert.ToChar(Console.ReadLine());
						Console.Write("Give me another number: ");

						numeroStr = Console.ReadLine();

						if (!checkIsInRange(numeroStr))
						{

							operand2 = Convert.ToInt32(numeroStr);


						}

						while (checkIsInRange(numeroStr))
						{

							Console.Write("Give me another number: ");
							numeroStr = Console.ReadLine();

							if (!checkIsInRange(numeroStr))
							{

								operand2 = Convert.ToInt32(numeroStr);


							}


						}


						if (operation == '/' && operand2 == 0)
						{
							Console.WriteLine("Math Error");

						}

						calc.addOperation(operation, operand1, operand2);

						Console.WriteLine();

						while (keepCalcQuestion)
						{

							Console.Write("Keep using calculator?'Y' for YES 'N' for NO): ");
							yesNo = Console.ReadLine();

							if (yesNo == "Y" || yesNo == "y")
							{
								keepCalc = false;
								keepCalcQuestion = false;
								Console.Clear();
							}

							if (yesNo == "N"|| yesNo == "n")
							{
								Console.Clear();
								Console.WriteLine("Bye bye!");
								Console.ReadKey();
								keepCalc = true;
								keepCalcQuestion = false;

							}
						}
						break;
					case 2://Elimina una operación

						Console.Clear();

						Console.Write("What operation do you want to delete?(1 to 10): ");
						position = Convert.ToInt32(Console.ReadLine());
								calc.deleteOperation(position - 1);


						

						// Console.WriteLine("Operation " + position + " deleted.");
						while (keepCalcQuestion)
						{

							Console.Write("Keep using calculator?'Y' for YES 'N' for NO): ");
							yesNo = Console.ReadLine();

							if (yesNo == "Y"|| yesNo == "y")
							{
								keepCalc = false;
								keepCalcQuestion = false;
								Console.Clear();
							}

							if (yesNo == "N"|| yesNo == "n")
							{
								Console.Clear();
								Console.WriteLine("Bye bye!");
								Console.ReadKey();
								keepCalc = true;
								keepCalcQuestion = false;

							}
						}
						break;
					case 3://Te da el resultado de una operación

						Console.Clear();

						Console.Write("What operation do you want to evaluate?(1 to 10): ");
						position = Convert.ToInt32(Console.ReadLine());

						
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine(calc.evalOperation(position - 1));
						Console.ForegroundColor = ConsoleColor.Yellow;

						while (keepCalcQuestion)
						{

							Console.Write("Keep using calculator?'Y' for YES 'N' for NO): ");
							yesNo = Console.ReadLine();

							if (yesNo == "Y"|| yesNo == "y")
							{
								keepCalc = false;
								keepCalcQuestion = false;
								Console.Clear();
							}

							if (yesNo == "N"|| yesNo == "n")
							{
								Console.Clear();
								Console.WriteLine("Bye bye!");
								Console.ReadKey();
								keepCalc = true;
								keepCalcQuestion = false;

							}
						}
						break;
					case 4://Enseña una operación

						Console.Clear();

						Console.Write("What operation do you want to see?(1 to 10): ");
						position = Convert.ToInt32(Console.ReadLine());

						calc.printOperation(position - 1);

						while (keepCalcQuestion)
						{

							Console.Write("Keep using calculator?'Y' for YES 'N' for NO): ");
							yesNo = Console.ReadLine();

							if (yesNo == "Y"|| yesNo == "y")
							{
								keepCalc = false;
								keepCalcQuestion = false;
								Console.Clear();
							}

							if (yesNo == "N"|| yesNo == "n")
							{
								Console.Clear();
								Console.WriteLine("Bye bye!");
								Console.ReadKey();
								keepCalc = true;
								keepCalcQuestion = false;

							}
						}
						break;
					case 5://Enseña todas las operaciones

						Console.Clear();

						Console.ForegroundColor = ConsoleColor.Green;
						calc.printAllOperations();
						Console.ForegroundColor = ConsoleColor.Yellow;
						while (keepCalcQuestion)
						{

							Console.Write("Keep using calculator?'Y' for YES 'N' for NO): ");
							yesNo = Console.ReadLine();

							if (yesNo == "Y"|| yesNo == "y")
							{
								keepCalc = false;
								keepCalcQuestion = false;
								Console.Clear();
							}

							if (yesNo == "N"|| yesNo == "n")
							{

								Console.Clear();
								Console.WriteLine("Bye bye!");
								Console.ReadKey();
								keepCalc = true;
								keepCalcQuestion = false;

							}
						}
						break;
					case 6://Enseña la operación con el resultado más alto

						Console.Clear();
						Console.ForegroundColor = ConsoleColor.Green;
						calc.printHigherResultOperation();
						Console.ForegroundColor = ConsoleColor.Yellow;

						while (keepCalcQuestion)
						{

							Console.Write("Keep using calculator?'Y' for YES 'N' for NO): ");
							yesNo = Console.ReadLine();

							if (yesNo == "Y"|| yesNo == "y")
							{
								keepCalc = false;
								keepCalcQuestion = false;
								Console.Clear();
							}

							if (yesNo == "N"|| yesNo == "n")
							{
								Console.Clear();
								Console.WriteLine("Bye bye!");
								Console.ReadKey();
								keepCalc = true;
								keepCalcQuestion = false;

							}
						}
						break;
					case 7://Enseña una operación con el resultado más alto dependiendo del signo que le des

						Console.Clear();


						Console.Write("What higher result sign do you want to see?('+' , '-' , '*' , '/'): ");
						operation = Convert.ToChar(Console.ReadLine());



						Console.ForegroundColor = ConsoleColor.Green;
						calc.printHigherResultOperation(operation);
						Console.ForegroundColor = ConsoleColor.Yellow;

						while (keepCalcQuestion)
						{

							Console.Write("Keep using calculator?'Y' for YES 'N' for NO): ");
							yesNo = Console.ReadLine();

							if (yesNo == "Y"|| yesNo == "y")
							{
								keepCalc = false;
								keepCalcQuestion = false;
								Console.Clear();
							}

							if (yesNo == "N"|| yesNo == "n")
							{
								Console.Clear();
								Console.WriteLine("Bye bye!");
								Console.ReadKey();
								keepCalc = true;
								keepCalcQuestion = false;

							}
						}
						break;
					case 8://Dice si hay números negativos

						calc.anyNegativeOperation();

						if (calc.anyNegativeOperation())
						{
							Console.ForegroundColor = ConsoleColor.Green;
							Console.WriteLine("Yes, there is at least one negative operation\n");
							Console.ForegroundColor = ConsoleColor.Yellow;
						}
						else {
							Console.ForegroundColor = ConsoleColor.Red;
							Console.WriteLine("Nope, there aren't negative numbers\n");
							Console.ForegroundColor = ConsoleColor.Yellow;

						}

						while (keepCalcQuestion)
						{

							Console.Write("Keep using calculator?'Y' for YES 'N' for NO): ");
							yesNo = Console.ReadLine();

							if (yesNo == "Y"|| yesNo == "y")
							{
								keepCalc = false;
								keepCalcQuestion = false;
								Console.Clear();
							}

							if (yesNo == "N"|| yesNo == "n")
							{
								Console.Clear();
								Console.WriteLine("Bye bye!");
								Console.ReadKey();
								keepCalc = true;
								keepCalcQuestion = false;

							}
						}
						break;
					case 9://Exit
						keepCalc = true;

						Console.Clear();
						Console.WriteLine("Bye bye!");
						Console.ReadKey();


						break;
				}
			}
		}

	public static bool checkIsInRange(string value)
	{

		int i = 0;
		bool found = false;

		while (i < value.Length && !found)
		{

			int ascii = Convert.ToInt32(value[i]);

			if (ascii < 48 || ascii > 57)
			{
				found = true;
			}
			else {

				i++;
			}
		}
		return found;
		}
	}
}

