<template>
    <ul class="nav">
        <li class="nav-item" v-for="(tab, index) in tabs" :key="index">
            <a
                class="nav-link"
                :href="getUrl(tab)"
                :class="{ 'active bg-white': tab.active }"
                @click.prevent="tabClick(tab)"
                >{{ tab.title }}</a
            >
        </li>
    </ul>
</template>
<script>
export default {
    props: {
        id: String,
        data: Array
    },
    data: function() {
        return {
            tabs: [...this.data]
        };
    },
    computed: {},
    methods: {
        getUrl: function(tab) {
            if (this.id) {
                return '#' + this.id + '-' + tab.id;
            } else {
                return '#' + tab.id;
            }
        },
        tabClick: function(tab) {
            if (tab.active) {
                return;
            }

            if (tab.onClick && typeof tab.onClick == 'function') {
                tab.onClick();
            }

            this.tabs = this.tabs.reduce((acc, item) => {
                if (item.id == tab.id) {
                    return [...acc, { ...item, active: true }];
                } else {
                    return [...acc, { ...item, active: false }];
                }
            }, []);

            let activePanel = document.querySelector(this.getUrl(tab));

            if (activePanel) {
                let old = activePanel.parentElement.querySelector(
                    '.tab-pane.active'
                );

                if (old) {
                    old.classList.remove('show');
                    setTimeout(() => {
                        old.classList.remove('active');
                    }, 150);
                }

                activePanel.classList.add('show');
                setTimeout(() => {
                    activePanel.classList.add('active');
                }, 150);
            }
        }
    }
};
</script>
