let editor = null;
const Editor = toastui.Editor;

const initializeEditor = async (selector, initialValue, onInitialized) => {
    disposeEditor();

    const textArea = await waitForElement(selector);
    if (textArea) {
        editor = new Editor({
            el: textArea,
            height: '500px',
            initialEditType: 'markdown',
            previewStyle: 'vertical'
        });

        setValue(initialValue);

        DotNet.invokeMethodAsync('MyBlog', onInitialized, true);
    } else {
        DotNet.invokeMethodAsync('MyBlog', onInitialized, false);
    }
}

const disposeEditor = () => {
    if (editor) {
        editor.destroy();
        editor = null;
    }
}

const getValue = () => {
    return editor ? editor.getMarkdown() : "";
}

const setValue = (value) => {
    if (editor) {
        editor.setMarkdown(value, true);
    }
}

const waitForElement = (selector) => {
    return new Promise(resolve => {
        const element = document.querySelector(selector);
        if (element) {
            return resolve(element);
        }

        const observer = new MutationObserver(_ => {
            const elem = document.querySelector(selector);
            if (elem) {
                resolve(elem);
                observer.disconnect();
            }
        });

        observer.observe(document.body, {
            childList: true,
            subtree: true
        });
    });
}

window.app = Object.assign({}, window.app, {
    initializeEditor,
    disposeEditor,
    getValue,
    setValue
});
