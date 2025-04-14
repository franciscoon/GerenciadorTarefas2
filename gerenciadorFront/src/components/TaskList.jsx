import { TaskItem } from "./TaskItem";
import "../assets/css/TaskList.css";

export function TaskList({ tarefas, onEditar, onExcluir, onVer }) {
  if (tarefas.length === 0) {
    return <div className="task-list-container"><p>Nenhuma tarefa encontrada</p></div>;
  }

  return (
    <div className="task-list-container">
      <ul style={{ marginTop: "1rem", listStyle: "none", padding: 0 }}>
        {tarefas.map((tarefa) => (
          <TaskItem
            key={tarefa.id}
            tarefa={tarefa}
            onEditar={() => onEditar(tarefa)}
            onExcluir={() => onExcluir(tarefa.id)}
            onVer={() => onVer(tarefa.id)}
          />
        ))}
      </ul>
    </div>
  );
}
