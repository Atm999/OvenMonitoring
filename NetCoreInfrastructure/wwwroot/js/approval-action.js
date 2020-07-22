if (typeof (approvalAction) === "undefined") {
    var approvalAction = {
        Agree: { label: "同意", value:0},
        Reject: { label: "拒绝", value: 1 },
        Recall: { label: "撤回", value: 2 },
        Add: { label: "添加", value: 3 },
        Repair: { label: "维修", value: 4 },
        ReEdit: { label: "重新编辑", value: 5 }
    };
}

if (typeof (repairStatus) === "undefined") {
    var repairStatus = {
        Repairing: {label:"待维修",value:0 },
        Repaired: {label:"已维修,待验收",value:1 },
        Edit: {label:"已撤回",value:2 },
        Complete: { label: "已完成", value: 3 },
    };
}