import { useState, useEffect } from "react";
import { api } from "../services/api";
import {toast} from "react-toastify";
import "../assets/css/modal.css";

export function TaskFormModal({tarefa, onTarefaCriadaOuEditada, onClose}){
    const [titulo, setTitulo] = useState("");
    const [descricao, setDescricao] = useState("");
    const [dataConclusao, setDataConclusao] = useState("");
    const [status, setStatus] = useState("Pendente");

    const statusOptions = [
        { value: "Pendente", label: "Pendente" },
        { value: "EmProgresso", label: "Em Progresso" },
        { value: "Concluida", label: "Concluída" },
      ];

    useEffect(() => {
        if(tarefa){
            setTitulo(tarefa.titulo || "");
            setDescricao(tarefa.descricao || "");
            setDataConclusao(tarefa.dataConclusao ? tarefa.dataConclusao.slice(0, 10) : "");
            setStatus(tarefa.status || "Pendente");
        }
        else{
            setTitulo("");
            setDescricao("");
            setDataConclusao("");
            setStatus("Pendente");
        }
    }, [tarefa]);

    async function handleSubmit(e) {
        e.preventDefault();

        try{
            if(tarefa){
                await api.put(`/tarefas/${tarefa.id}`, {
                    titulo,
                    descricao,
                    dataConclusao: dataConclusao || null,
                    status,
                });

                toast.success("Tarefa Editada com Sucesso !");
            }
            else{
                await api.post("/tarefas", {
                    titulo,
                    descricao,
                    dataConclusao: dataConclusao || null,
                    status,
                    dataCriacao: new Date()
                });

                toast.success("Tarefa criada com Sucesso !");
            }

            if(onTarefaCriadaOuEditada ){
                onTarefaCriadaOuEditada ();
            }
        } 
        catch(error){
            console.error("Erro ao criar tarefa:", error);

            const mensagem = error.response?.data?.message || "Erro inesperado. Tente novamente";
            toast.error(mensagem);
        }
    }

    return(
        <div className="modal-overlay">
            <div className="modal-content"> 
                <button className="close-button" onClick={onClose}>X</button>
                <h2>{tarefa ? "Editar Tarefa" : "Nova Tarefa"}</h2>
                
                <form onSubmit={handleSubmit} style={{marginBottom: "2rem"}}>
                    <div className="form-field">
                        <input 
                            type="text"
                            placeholder="Título"
                            value={titulo}
                            onChange={(e) => setTitulo(e.target.value)}
                            required 
                        />
                    </div>
                        
                    <div className="form-field">
                        <textarea 
                            placeholder="Descrição"
                            value={descricao}
                            onChange={(e) => setDescricao(e.target.value)}>
                        </textarea>
                    </div>

                    <div className="form-field">
                        <label>Data de Conclusão:</label>
                        <input
                            type="date"
                            value={dataConclusao}
                            onChange={(e) => setDataConclusao(e.target.value)}
                        />
                    </div>

                    <div className="form-field">
                        <label>Status:</label>
                        <select value={status} onChange={(e) => setStatus(e.target.value)}>
                            {statusOptions.map((option) => (
                                <option key={option.value} value={option.value}>{option.label}</option>
                            ))}
                        </select>
                    </div>

                    <div className="modal-buttons">
                        <button className="submit-button" type="submit">{tarefa ? "Salvar" : "Adicionar"}</button>
                        <button className="cancel-button" type="button" onClick={onClose}>Cancelar</button>
                    </div>
                </form>
            </div>
        </div>
    )
}