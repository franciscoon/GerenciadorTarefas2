export function formatStatus(status) {
  if (status === "EmProgresso") {
    return "Em Progresso";
  } else if (status === "Concluido") {
    return "Concluído";
  }
  return status; 
}