import "../assets/css/TaskItem.css";
import {formatStatus} from "../utils/utils.js"

export function TaskItem({ tarefa, onEditar, onExcluir, onVer }) {
  return (
    <li className="task-item">
      <div className="task-info">
        <div className="task-title">
          <strong>Título:</strong> {tarefa.titulo}
        </div>
        <div className="task-status">
          <strong>Status:</strong> {formatStatus(tarefa.status)}
        </div>
      </div>
      <div className="task-item-buttons">
        <button onClick={onVer}>Ver</button>
        <button onClick={onEditar}>Editar</button>
        <button onClick={onExcluir}>Excluir</button>
      </div>
    </li>
  );
}
